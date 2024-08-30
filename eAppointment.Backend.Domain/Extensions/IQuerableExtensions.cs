using eAppointment.Backend.Domain.Helpers;
using System.Linq.Expressions;
using System.Reflection;

namespace eAppointment.Backend.Domain.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> LazyGlobalFilter<T>(this IQueryable<T> qry, LazyLoadEvent2 lle, params Expression<Func<T, string>>[] fields)
        {
            if (string.IsNullOrWhiteSpace(lle.globalFilter))
            {
                return qry;
            }

            var parameter = Expression.Parameter(typeof(T), "x");

            Expression predicate = null;

            foreach (var field in fields)
            {
                var member = Expression.Invoke(field, parameter);

                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                var containsExpression = Expression.Call(member, containsMethod, Expression.Constant(lle.globalFilter));

                if (predicate == null)
                {
                    predicate = containsExpression;
                }

                else
                {
                    predicate = Expression.OrElse(predicate, containsExpression);
                }
            }

            if (predicate == null)
            {
                return qry;
            }

            var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);

            return qry.Where(lambda);
        }

        public static IQueryable<T> LazyOrderBy2<T>(this IQueryable<T> qry, LazyLoadEvent2 lle)
        {
            if (!string.IsNullOrEmpty(lle.sortField))
            {
                return qry.OrderBy<T>(lle.sortField, lle.sortOrder);
            }

            return qry;
        }

        public static IQueryable<T> LazyOrderBy<T>(this IQueryable<T> qry, LazyLoadEvent lle)
        {
            if (string.IsNullOrEmpty(lle.sortField))
            {
                return qry.OrderBy<T>(lle.sortField, lle.sortOrder);
            }

            return qry;
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> qry, string sortField, int sortOrder)
        {
            if (!string.IsNullOrEmpty(sortField))
            {
                ParameterExpression _parm = Expression.Parameter(typeof(T));
                MemberExpression memberAccess = Expression.PropertyOrField(_parm, sortField);
                LambdaExpression keySelector = Expression.Lambda(memberAccess, _parm);

                MethodCallExpression orderBy = Expression.Call(
                    typeof(Queryable),
                    (sortOrder == 1 ? "OrderBy" : "OrderByDescending"),
                    new Type[] { typeof(T), memberAccess.Type },
                    qry.Expression,
                    Expression.Quote(keySelector));

                return qry.Provider.CreateQuery<T>(orderBy);
            }

            return qry;
        }

        public static IQueryable<T> LazySkipTake2<T>(this IQueryable<T> qry, LazyLoadEvent2 lle)
        {
            return qry.SkipTake<T>((int)lle.first, (int)lle.rows);
        }

        public static IQueryable<T> LazySkipTake<T>(this IQueryable<T> qry, LazyLoadEvent lle)
        {
            return qry.SkipTake<T>((int)lle.first, (int)lle.rows);
        }

        private static IQueryable<T> SkipTake<T>(this IQueryable<T> qry, int first, int rows)
        {
            if (rows > 0)
            {
                qry = qry.Skip((int)first).Take(rows);
            }

            return qry;
        }

        public static IQueryable<T> LazyFilters<T>(this IQueryable<T> qry, LazyLoadEvent lle)
        {
            if (lle.filters != null && lle.filters.Count > 0)
            {
                foreach (var _o in lle.filters)
                {
                    PropertyInfo _propertyInfo = typeof(T).GetProperty(_o.Key);
                    Type _type = _propertyInfo.PropertyType;
                    FilterMetadata _value = _o.Value;

                    if (_value != null)
                    {
                        if (_value.value != null)
                        {
                            var whereClause = LazyDynamicFilterExpression<T>(_o.Key,
                                    (string)_value.matchMode, _value.value.ToString(), _type, "");

                            qry = qry.Where(whereClause);
                        }
                    }
                }
            }

            return qry;
        }

        public static IQueryable<T> LazyFilters2<T>(this IQueryable<T> qry, LazyLoadEvent2 lle)
        {
            if (lle.filters != null && lle.filters.Count > 0)
            {
                foreach (var _f in lle.filters)
                {
                    if (_f.Key == null || _f.Value == null)
                    {
                        continue;
                    }

                    PropertyInfo _propertyInfo = typeof(T).GetProperty(_f.Key);
                    Type _type = _propertyInfo.PropertyType;
                    FilterMetadata[] _values = _f.Value.Where(f => f.value != null).ToArray();

                    if (_values != null && _values.Length != 0)
                    {
                        var count = 1;

                        Expression<Func<T, bool>> whereClause = null;

                        foreach (FilterMetadata _m in _values)
                        {
                            var whereTemp = LazyDynamicFilterExpression<T>(_f.Key,
                                    (string)_m.matchMode, _m.value.ToString(), _type, _m.@operator);

                            if (count == 1)
                            {
                                whereClause = whereTemp;
                            }

                            else
                            {
                                if (_m.@operator.ToLower() == "or")
                                {
                                    whereClause = whereClause.OrExpression(whereTemp);
                                }

                                else
                                {
                                    whereClause = whereClause.AndExpression(whereTemp);
                                }
                            }

                            count++;
                        }

                        qry = qry.Where(whereClause);
                    }
                }
            }

            return qry;
        }

        public static Expression<Func<T, bool>> OrExpression<T>(
            this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(left.Body, invokedExpr), left.Parameters);
        }

        public static Expression<Func<T, bool>> AndExpression<T>(
            this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }

        private static Expression<Func<TEntity, bool>>
            LazyDynamicFilterExpression<TEntity>(
                string propertyName, string match, string value, Type valueType, string op)
        {
            Type type = typeof(TEntity);

            object asType = AsType(value, valueType);

            ParameterExpression p = Expression.Parameter(type, type.Name);
            MemberExpression member = Expression.Property(p, propertyName);

            string _stringValue = asType.ToString();

            ConstantExpression valueExpression = Expression.Constant(asType);

            MethodInfo method;
            Expression q;

            switch (match.ToLower())
            {
                case "gt":
                    q = Expression.GreaterThan(member, valueExpression);
                    break;
                case "lt":
                    q = Expression.LessThan(member, valueExpression);
                    break;
                case "equals":
                    q = Expression.Equal(member, valueExpression);
                    break;
                case "lte":
                    q = Expression.LessThanOrEqual(member, valueExpression);
                    break;
                case "gte":
                    q = Expression.GreaterThanOrEqual(member, valueExpression);
                    break;
                case "notequals":
                    q = Expression.NotEqual(member, valueExpression);
                    break;
                case "contains":
                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    q = Expression.Call(member, method ?? throw new InvalidOperationException(),
                        Expression.Constant(_stringValue, typeof(string)));
                    break;
                case "startswith":
                    method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                    q = Expression.Call(
                        member,
                        method ?? throw new InvalidOperationException(),
                        Expression.Constant(_stringValue, typeof(string)));
                    break;
                case "endswith":
                    method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                    q = Expression.Call(member, method ?? throw new InvalidOperationException(),
                        Expression.Constant(_stringValue, typeof(string)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(match), $"filter matchMode of: '{match}', not gt/lt/equals/lte/gte/notequals/contains/startswith/endswith");
            }

            return Expression.Lambda<Func<TEntity, bool>>(q, p);
        }

        private static object AsType(string value, Type type)
        {
            string v = value;

            if (value.StartsWith("'") && value.EndsWith("'"))
                v = value.Substring(1, value.Length - 2);

            if (value.StartsWith("\"") && value.EndsWith("\""))
                v = value.Substring(1, value.Length - 2);

            if (type == typeof(string)) return v;
            if (type == typeof(DateTime)) return DateTime.Parse(v);
            if (type == typeof(DateTime?)) return DateTime.Parse(v);
            if (type == typeof(int)) return int.Parse(v);
            if (type == typeof(int?)) return int.Parse(v);
            if (type == typeof(long) || type == typeof(long?)) return long.Parse(v);
            if (type == typeof(short) || type == typeof(short?)) return short.Parse(v);
            if (type == typeof(byte) || type == typeof(byte?)) return byte.Parse(v);
            if (type == typeof(bool) || type == typeof(bool?)) return bool.Parse(v);

            throw new ArgumentException("NSG.PrimeNG.LazyLoading.Helpers.AsType: " +
                "A filter was attempted for a field with value '" + value + "' and type '" +
                type + "' however this type is not currently supported");
        }
    }
}
