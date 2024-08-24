using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Infrastructure.Context;

namespace eAppointment.Backend.Infrastructure.Concretes
{
    internal class CountyRepository : Repository<County>, ICountyRepository
    {
        public CountyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
