using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using eAppointment.Backend.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Backend.Infrastructure.Repositories
{
    internal class ErrorLogRepository : Repository<ErrorLog, ApplicationDbContext>, IErrorLogRepository
    {
        public ErrorLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
