using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Helpers;
using eAppointment.Backend.Domain.Repositories;
using eAppointment.Backend.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eAppointment.Backend.WebAPI.Middlewares
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _provider;

        public AuditMiddleware(RequestDelegate next, IServiceProvider provider)
        {
            _next = next;
            _provider = provider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                using (var scope = _provider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                    var tableLogRepository = scope.ServiceProvider.GetRequiredService<ITableLogRepository>();

                    var auditLogId = await AddAuditLogAsync(context, auditLogRepository);

                    await AddTableLogAsync(dbContext, tableLogRepository, auditLogId);
                }
            }

            catch (Exception exception)
            {
                using (var scope = _provider.CreateScope())
                {
                    var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                    var errorLogRepository = scope.ServiceProvider.GetRequiredService<IErrorLogRepository>();

                    var auditLogId = await AddAuditLogAsync(context, auditLogRepository);

                    await AddErrorLogAsync(exception, errorLogRepository, auditLogId);
                }
            }

            finally
            {
                using (var scope = _provider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    await unitOfWork.SaveChangesAsync();
                }
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();

            request.Body.Position = 0;

            var reader = new StreamReader(request.Body);

            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0;

            return body;
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }

        private string GetQueryParameters(IQueryCollection queryCollection)
        {
            var queryParameters = new Dictionary<string, string>();

            foreach (var query in queryCollection)
            {
                queryParameters.Add(query.Key, query.Value);
            }

            return JsonSerializer.Serialize(queryParameters);
        }

        private string GetHeaders(IHeaderDictionary headers)
        {
            var headerDictionary = new Dictionary<string, string>();

            foreach (var header in headers)
            {
                headerDictionary.Add(header.Key, header.Value);
            }

            return JsonSerializer.Serialize(headerDictionary);
        }

        private async Task AddErrorLogAsync(Exception exception, IErrorLogRepository errorLogRepository, int auditLogId)
        {
            if (exception != null)
            {
                var errorLog = new ErrorLog()
                {
                    ExceptionMessage = exception.Message,
                    ExceptionStackTrace = exception.StackTrace,
                    AuditLogId = auditLogId
                };

                if (exception.InnerException != null)
                {
                    errorLog.InnerExceptionMessage = exception.InnerException.Message;
                    errorLog.InnerExceptionStackTrace = exception.InnerException.StackTrace;
                }

                await errorLogRepository.AddAsync(errorLog);
            }
        }

        private async Task AddTableLogAsync(ApplicationDbContext dbContext, ITableLogRepository tableLogRepository, int auditLogId)
        {
            var tableChangeEntries = new List<TableChangeEntry>();

            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                if (entry.Entity is TableLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var tableChangeEntry = new TableChangeEntry(entry);

                tableChangeEntry.TableName = entry.Entity.GetType().Name;

                tableChangeEntries.Add(tableChangeEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        int propValue = (int)property.CurrentValue;

                        if (propertyName == "Id" && propValue < 0)
                        {
                            property.CurrentValue = 0;
                        }

                        tableChangeEntry.KeyValues[propertyName] = property.CurrentValue;

                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:

                            tableChangeEntry.TableChangeType = TableChangeType.Create;
                            tableChangeEntry.NewValues[propertyName] = property.CurrentValue;

                            break;

                        case EntityState.Deleted:

                            tableChangeEntry.TableChangeType = TableChangeType.Delete;
                            tableChangeEntry.OldValues[propertyName] = property.OriginalValue;

                            break;

                        case EntityState.Modified:

                            if (property.IsModified)
                            {
                                tableChangeEntry.AffectedColumns.Add(propertyName);
                                tableChangeEntry.TableChangeType = TableChangeType.Update;
                                tableChangeEntry.OldValues[propertyName] = property.OriginalValue;
                                tableChangeEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var tableChangeEntry in tableChangeEntries)
            {
                await tableLogRepository.AddAsync(tableChangeEntry.ToTableLog(auditLogId));
            }
        }

        private async Task<int> AddAuditLogAsync(HttpContext context, IAuditLogRepository auditLogRepository)
        {
            var auditLog = new AuditLog
            {
                Method = context.Request.Method,
                Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
                Path = context.Request.Path,
                QueryParameters = GetQueryParameters(context.Request.Query),
                RequestHeaders = GetHeaders(context.Request.Headers),
                RequestBody = await ReadRequestBodyAsync(context.Request),
                StatusCode = context.Response.StatusCode,
                ResponseHeaders = GetHeaders(context.Response.Headers),
                ResponseBody = await ReadResponseBodyAsync(context.Response),
                UserName = context.User.Identity.Name ?? "Anonymous",
                RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString(),
                LocalIpAddress = context.Connection.LocalIpAddress?.ToString(),
                RemotePort = context.Connection.RemotePort,
                LocalPort = context.Connection.LocalPort,
                Timestamp = DateTime.UtcNow
            };

            await auditLogRepository.AddAsync(auditLog);

            var lastRecord = await auditLogRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            return lastRecord.Id;
        }
    }
}