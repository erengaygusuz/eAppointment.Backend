using eAppointment.Backend.Domain.Entities;

namespace eAppointment.Backend.Domain.Abstractions
{
    public interface IAuditLogRepository : IRepository<AuditLog>
    {
    }
}
