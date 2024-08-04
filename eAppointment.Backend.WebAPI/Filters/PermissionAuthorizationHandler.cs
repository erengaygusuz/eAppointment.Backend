using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace eAppointment.Backend.WebAPI.Filters
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            var canAccess = context.User.Claims
                .Any(c => c.Type == "Permissions" && JsonSerializer.Deserialize<List<string>>(c.Value).Contains(requirement.Permission));

            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
