using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Infrastructure.Context;

namespace eAppointment.Backend.Infrastructure.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _appDbContext;

        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            var result = await _appDbContext.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
