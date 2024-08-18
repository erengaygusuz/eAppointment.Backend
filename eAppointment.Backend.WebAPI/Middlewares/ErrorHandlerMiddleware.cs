using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<object> _localization;

        public ErrorHandlerMiddleware(RequestDelegate next, IStringLocalizer<object> localization)
        {
            _next = next;
            _localization = localization;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == 401)
                {
                    await context.Response.WriteAsJsonAsync(Result<string>.Failure(401, _localization["ErrorsCodes.401"].Value));
                }

                else if(context.Response.StatusCode == 403)
                {
                    await context.Response.WriteAsJsonAsync(Result<string>.Failure(403, _localization["ErrorsCodes.403"].Value));
                }
            }

            catch (Exception exception)
            {
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(Result<string>.Failure(500, _localization["ErrorsCodes.500"].Value));
            }
        }
    }
}