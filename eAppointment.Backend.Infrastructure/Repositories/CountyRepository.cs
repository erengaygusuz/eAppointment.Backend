using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using eAppointment.Backend.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Backend.Infrastructure.Repositories
{
    internal class CountyRepository : Repository<County, ApplicationDbContext>, ICountyRepository
    {
        public CountyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
