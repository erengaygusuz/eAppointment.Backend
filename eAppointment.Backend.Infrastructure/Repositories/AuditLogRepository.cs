using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using eAppointment.Backend.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Backend.Infrastructure.Repositories
{
    internal class AuditLogRepository : Repository<AuditLog, ApplicationDbContext>, IAuditLogRepository
    {
        public AuditLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
