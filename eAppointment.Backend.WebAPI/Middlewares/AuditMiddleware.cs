using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
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
            var requestBody = await ReadRequestBodyAsync(context.Request);

            var originalResponseBodyStream = context.Response.Body;

            var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                var responseBodyContent = await ReadResponseBodyAsync(context.Response);

                await LogRequestResponseAsync(context, requestBody, responseBodyContent);

                await responseBody.CopyToAsync(originalResponseBodyStream);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                context.Response.Body = originalResponseBodyStream;
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

        private async Task LogRequestResponseAsync(HttpContext context, string requestBody, string responseBody)
        {
            var auditLog = new AuditLog
            {
                TimeStamp = DateTime.UtcNow,
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                RequestBody = requestBody,
                ResponseBody = responseBody,
                StatusCode = context.Response.StatusCode,
                Headers = JsonSerializer.Serialize(context.Request.Headers),
                IPAddress = context.Connection.RemoteIpAddress?.ToString()
            };

            using (var scope = _provider.CreateScope())
            {
                var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                await auditLogRepository.AddAsync(auditLog);

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}