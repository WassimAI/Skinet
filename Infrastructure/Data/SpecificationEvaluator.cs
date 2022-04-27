using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //This class handles querying our data and returns it back to the controller
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity // we could have used T but wanted to be more explicit
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // this is to simply apply the criteria on that query! and return the updated query
            }

            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy); // this is to simply apply the criteria on that query! and return the updated query
            }

            if(spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending); // this is to simply apply the criteria on that query! and return the updated query
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take); //ordering here is important - so paging comes after filtering and sorting
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}