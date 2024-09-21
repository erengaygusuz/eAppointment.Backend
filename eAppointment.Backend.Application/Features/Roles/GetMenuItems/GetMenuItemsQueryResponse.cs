using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Roles.GetMenuItems
{
    public sealed record GetMenuItemsQueryResponse(
        List<MenuTreeItem> items);
}
