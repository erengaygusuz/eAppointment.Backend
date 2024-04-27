using eAppointment.Backend.Domain.Entities;
using GenericRepository;

namespace eAppointment.Backend.Domain.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
    }
}
