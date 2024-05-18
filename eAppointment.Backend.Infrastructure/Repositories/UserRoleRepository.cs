using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using eAppointment.Backend.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Backend.Infrastructure.Repositories
{
    internal sealed class UserRoleRepository : Repository<AppUserRole, ApplicationDbContext>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
