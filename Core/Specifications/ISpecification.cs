using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
         Expression<Func<T, bool>> Criteria {get;} // for our expressions (x => x.id == id)
         List<Expression<Func<T, object>>> Includes {get;} // for our inclide statements
         Expression<Func<T, object>> OrderBy {get;}
         Expression<Func<T, object>> OrderByDescending {get;}
         int Take {get;}
         int Skip {get;}
         bool IsPagingEnabled {get;}
    }
}