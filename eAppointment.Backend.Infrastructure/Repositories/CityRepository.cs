using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using eAppointment.Backend.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Backend.Infrastructure.Repositories
{
    internal class CityRepository : Repository<City, ApplicationDbContext>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
