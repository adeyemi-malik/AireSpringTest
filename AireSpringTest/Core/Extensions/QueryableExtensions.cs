using AireSpringTest.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AireSpringTest.Data;
using Microsoft.EntityFrameworkCore;

namespace AireSpringTest.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageIndex,
            int limit, string? sortColumn = null)
        {
            int totalCount;

            try
            {
                totalCount = await query.CountAsync();
            }
            catch (InvalidOperationException)
            {
                var list = query.ToList();
                query = list.AsQueryable();
                totalCount = list.Count;
            }

            var collection = query;
            if (sortColumn != null)
            {
                collection = query
                    .OrderBy(sortColumn, false);
            }

            collection = collection.Skip((pageIndex - 1) * limit)
                                   .Take(limit);
            return new PaginatedList<T>(collection, totalCount);
        }

        public static IQueryable<Employee> SearchEmployees(this IQueryable<Employee> query,
            string? searchFilter = default)
        {
            if (searchFilter == null)
            {
                return query;
            }

            return query.Where(e => e.PhoneNumber == searchFilter || e.LastName == searchFilter);
        }

        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T, TKey>(this IQueryable<T> query,
            int pageIndex, int limit, Expression<Func<T, TKey>> keySelector) where T : class => await query.ToPaginatedListAsync(pageIndex, limit, keySelector.Name);

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
            bool desc)
        {
            var command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
