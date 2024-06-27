using System.Linq.Expressions;
using System.Reflection;
using WebApiMovies.Data.Entities;

namespace WebApiMovies.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByDinamyc<T>(this IQueryable<T> queryable, string orderByQuery)
        {
            if (string.IsNullOrEmpty(orderByQuery))
            {
                return queryable;
            }

            var columnName = orderByQuery.Split('_')[0];
            var property = typeof(T).GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
           
            if(property is null)
            {
                return queryable;
            }

            var expressionParameter = Expression.Parameter(typeof(T), "x");
            var expressionProperty = Expression.Property(expressionParameter, property.Name);
            var orderExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(expressionProperty, typeof(object)), expressionParameter);

            if (orderByQuery.Contains("desc"))
            {
                return queryable.OrderByDescending(orderExpression);
            }


            return queryable.OrderBy(orderExpression);
        }
    }
}
