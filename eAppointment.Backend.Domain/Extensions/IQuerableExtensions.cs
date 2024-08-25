using Microsoft.EntityFrameworkCore;

namespace eAppointment.Backend.Domain.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(x => EF.Property<object>(x, propertyName));
        }

        public static IQueryable<T> OrderByDescendingProperty<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(x => EF.Property<object>(x, propertyName));
        }
    }
}
