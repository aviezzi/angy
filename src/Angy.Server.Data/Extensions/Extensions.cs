using System;
using System.Linq;
using System.Threading.Tasks;
using Angy.Model;
using Angy.Server.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Angy.Server.Data.Extensions
{
    public static class Extensions
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : EntityBase
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult.Where(spec.Criteria);
        }

        public static async Task<T> CreateAsync<T>(this LuciferContext context, T entity) where T : EntityBase
        {
            var created = context.Attach(entity);
            await context.SaveChangesAsync();

            return created.Entity;
        }

        public static async Task<T> UpdateAsync<T>(this LuciferContext context, Guid id, T entity) where T : EntityBase
        {
            entity.Id = id;

            var old = await context.Set<T>().FindAsync(id);
            context.Entry(old).CurrentValues.SetValues(entity);

            await context.SaveChangesAsync();

            return entity;
        }
    }
}