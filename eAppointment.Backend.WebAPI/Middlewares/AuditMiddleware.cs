using eAppointment.Backend.Domain.Entities;
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
                using (var scope = _provider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                    var tableLogRepository = scope.ServiceProvider.GetRequiredService<ITableLogRepository>();

                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var result = await HandleBeforeRequestAsync(context, auditLogRepository, unitOfWork);

                    await _next(context);

                    try
                    {
                        await HandleAfterRequestAsync(context, auditLogRepository, unitOfWork, result);
                    }
                    finally
                    {
                        if (result.Item1 != null)
                        {
                            result.Item1.Dispose();
                        }

                        if (result.Item2 != null)
                        {
                            result.Item2.Dispose();
                        }
                    }
                    
                }
            }

            catch (Exception exception)
            {
                using (var scope = _provider.CreateScope())
                {
                    var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                    var errorLogRepository = scope.ServiceProvider.GetRequiredService<IErrorLogRepository>();

                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var lastRecord = await auditLogRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                    await AddErrorLogAsync(exception, errorLogRepository, unitOfWork, lastRecord.Id);
                }
            }
        }

        private async Task<Tuple<Stream, MemoryStream>> HandleBeforeRequestAsync(
            HttpContext context,
            IAuditLogRepository auditLogRepository,
            IUnitOfWork unitOfWork)
        {
            context.Request.EnableBuffering();

            var requestBodyText = await new StreamReader(context.Request.Body).ReadToEndAsync();

            context.Request.Body.Position = 0;

            var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            var auditLog = new AuditLog
            {
                Method = context.Request.Method,
                Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
                Path = context.Request.Path,
                QueryParameters = GetQueryParameters(context.Request.Query),
                RequestHeaders = GetHeaders(context.Request.Headers),
                RequestBody = requestBodyText,
                StatusCode = context.Response.StatusCode,
                ResponseHeaders = "",
                ResponseBody = "",
                UserName = context.User.Identity.Name ?? "Anonymous",
                RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString(),
                LocalIpAddress = context.Connection.LocalIpAddress?.ToString(),
                RemotePort = context.Connection.RemotePort,
                LocalPort = context.Connection.LocalPort,
                Timestamp = DateTime.Now
            };

            await auditLogRepository.AddAsync(auditLog);

            await unitOfWork.SaveChangesAsync();

            return Tuple.Create(context.Response.Body, responseBody);
        }

        private async Task HandleAfterRequestAsync(
            HttpContext context,
            IAuditLogRepository auditLogRepository,
            IUnitOfWork unitOfWork,
            Tuple<Stream, MemoryStream> streams)
        {
            streams.Item1.Seek(0, SeekOrigin.Begin);

            var responseBodyText = await new StreamReader(streams.Item1).ReadToEndAsync();

            streams.Item1.Seek(0, SeekOrigin.Begin);

            await streams.Item2.CopyToAsync(streams.Item1);

            var lastAuditLog = await auditLogRepository.GetAllWithTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            lastAuditLog.ResponseBody = responseBodyText;
            lastAuditLog.ResponseHeaders = GetHeaders(context.Response.Headers);

            //auditLogRepository.Update(lastAuditLog);

            await unitOfWork.SaveChangesAsync();
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

        private async Task AddErrorLogAsync(Exception exception, IErrorLogRepository errorLogRepository, IUnitOfWork unitOfWork, int auditLogId)
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

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}