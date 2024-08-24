namespace eAppointment.Backend.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
