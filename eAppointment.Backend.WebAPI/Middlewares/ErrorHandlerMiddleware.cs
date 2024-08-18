using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<object> _localization;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, IStringLocalizer<object> localization, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _localization = localization;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Request Started");

                await _next(context);

                if (context.Response.StatusCode == 401)
                {
                    _logger.LogError(_localization["ErrorsCodes.401"].Value);

                    await context.Response.WriteAsJsonAsync(Result<string>.Failure(401, _localization["ErrorsCodes.401"].Value));
                }

                else if(context.Response.StatusCode == 403)
                {
                    _logger.LogError(_localization["ErrorsCodes.403"].Value);

                    await context.Response.WriteAsJsonAsync(Result<string>.Failure(403, _localization["ErrorsCodes.403"].Value));
                }
            }

            catch (Exception exception)
            {
                _logger.LogError(exception.Message);

                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(Result<string>.Failure(500, _localization["ErrorsCodes.500"].Value));
            }
        }
    }
}