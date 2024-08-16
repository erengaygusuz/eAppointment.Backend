using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using eAppointment.Backend.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Backend.Infrastructure.Repositories
{
    internal class TableLogRepository : Repository<TableLog, ApplicationDbContext>, ITableLogRepository
    {
        public TableLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
