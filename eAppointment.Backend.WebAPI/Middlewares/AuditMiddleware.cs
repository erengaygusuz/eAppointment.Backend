using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
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
                var scope = _provider.CreateScope();

                var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                var tableLogRepository = scope.ServiceProvider.GetRequiredService<ITableLogRepository>();

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                await CreateAuditLogAsync(context, auditLogRepository, unitOfWork);

                await _next(context);

                await UpdateAuditLogAsync(context, auditLogRepository, unitOfWork);
            }

            catch (Exception exception)
            {
                var scope = _provider.CreateScope();

                var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                var errorLogRepository = scope.ServiceProvider.GetRequiredService<IErrorLogRepository>();

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var lastRecord = await auditLogRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                await CreateErrorLogAsync(exception, errorLogRepository, unitOfWork, lastRecord.Id);

                await UpdateAuditLogAsync(context, auditLogRepository, unitOfWork);
            }
        }

        private async Task CreateAuditLogAsync(
            HttpContext context,
            IAuditLogRepository auditLogRepository,
            IUnitOfWork unitOfWork)
        {
            var auditLog = new AuditLog
            {
                Method = context.Request.Method,
                Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
                Path = context.Request.Path,
                QueryParameters = GetQueryParameters(context.Request.Query),
                RequestHeaders = GetHeaders(context.Request.Headers),
                RequestBody = "",
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
        }

        private async Task UpdateAuditLogAsync(
            HttpContext context,
            IAuditLogRepository auditLogRepository,
            IUnitOfWork unitOfWork)
        {
            context.Request.EnableBuffering();

            var requestBodyText = await new StreamReader(context.Request.Body).ReadToEndAsync();

            context.Request.Body.Position = 0;

            var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            await responseBody.CopyToAsync(context.Response.Body);

            var lastAuditLog = await auditLogRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            lastAuditLog.Id = lastAuditLog.Id;
            lastAuditLog.Method = context.Request.Method;
            lastAuditLog.Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            lastAuditLog.Path = context.Request.Path;
            lastAuditLog.QueryParameters = GetQueryParameters(context.Request.Query);
            lastAuditLog.RequestHeaders = GetHeaders(context.Request.Headers);
            lastAuditLog.RequestBody = requestBodyText;
            lastAuditLog.StatusCode = context.Response.StatusCode;
            lastAuditLog.ResponseHeaders = GetHeaders(context.Response.Headers);
            lastAuditLog.ResponseBody = responseBodyText;
            lastAuditLog.UserName = context.User.Identity.Name ?? "Anonymous";
            lastAuditLog.RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
            lastAuditLog.LocalIpAddress = context.Connection.LocalIpAddress?.ToString();
            lastAuditLog.RemotePort = context.Connection.RemotePort;
            lastAuditLog.LocalPort = context.Connection.LocalPort;
            lastAuditLog.Timestamp = DateTime.Now;

            Console.WriteLine(lastAuditLog);

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

        private async Task CreateErrorLogAsync(Exception exception, IErrorLogRepository errorLogRepository, IUnitOfWork unitOfWork, int auditLogId)
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