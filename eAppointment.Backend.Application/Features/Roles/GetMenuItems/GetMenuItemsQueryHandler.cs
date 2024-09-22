using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Roles.GetMenuItems
{
    internal sealed class GetMenuItemsQueryHandler(
        RoleManager<Role> roleManager) : IRequestHandler<GetMenuItemsQuery, Result<GetMenuItemsQueryResponse>>
    {
        public async Task<Result<GetMenuItemsQueryResponse>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
        {
            List<Role> roles = await roleManager.Roles
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);

            var roleWithMenuItems = await roleManager.Roles.Where(x => x.Name == request.roleName).FirstOrDefaultAsync();

            var menuTreeItems = roleWithMenuItems.MenuItems.Where(x => x.ParentId == null).Select(x => new MenuTreeItem()
            {
                Label = x.MenuItemTranslations.FirstOrDefault(a => a.Language.Code == Thread.CurrentThread.CurrentCulture.Name).TranslationText,
                Icon = x.Icon,
                RouterLink = string.IsNullOrEmpty(x.RouterLink) ? null : new List<string>() { x.RouterLink },
                Items = x.Children != null ? x.Children.Select(x => new MenuTreeItem()
                {
                    Label = x.MenuItemTranslations.FirstOrDefault(a => a.Language.Code == Thread.CurrentThread.CurrentCulture.Name).TranslationText,
                    Icon = x.Icon,
                    RouterLink = string.IsNullOrEmpty(x.RouterLink) ? null : new List<string>() { x.RouterLink },
                    Items = x.Children != null ? x.Children.Select(x => new MenuTreeItem()
                    {
                        Label = x.MenuItemTranslations.FirstOrDefault(a => a.Language.Code == Thread.CurrentThread.CurrentCulture.Name).TranslationText,
                        Icon = x.Icon,
                        RouterLink = string.IsNullOrEmpty(x.RouterLink) ? null : new List<string>() { x.RouterLink },
                        Items = x.Children != null ? x.Children.Select(x => new MenuTreeItem()
                        {
                            Label = x.MenuItemTranslations.FirstOrDefault(a => a.Language.Code == Thread.CurrentThread.CurrentCulture.Name).TranslationText,
                            Icon = x.Icon,
                            RouterLink = string.IsNullOrEmpty(x.RouterLink) ? null : new List<string>() { x.RouterLink },
                            Items = x.Children != null ? x.Children.Select(x => new MenuTreeItem()
                            {
                                Label = x.MenuItemTranslations.FirstOrDefault(a => a.Language.Code == Thread.CurrentThread.CurrentCulture.Name).TranslationText,
                                Icon = x.Icon,
                                RouterLink = string.IsNullOrEmpty(x.RouterLink) ? null : new List<string>() { x.RouterLink }
                            }).ToList() : null
                        }).ToList() : null
                    }).ToList() : null
                }).ToList() : null
            }).ToList();

            var response = new GetMenuItemsQueryResponse(menuTreeItems);

            return response;
        }
    }
}
