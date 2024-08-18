using Serilog.Core;
using Serilog.Events;

namespace eAppointment.Backend.WebAPI.Helpers
{
    public class HttpContextEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is null)
            {
                return;
            }

            var httpContextModel = new HttpContextModel
            {
                Method = httpContext.Request.Method,
                CorrelationId = httpContext.TraceIdentifier
            };

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("HttpContext", httpContextModel, true));
        }
    }
    public class HttpContextModel
    {
        public string Method { get; init; }
        public string CorrelationId { get; init; }
    }
}
