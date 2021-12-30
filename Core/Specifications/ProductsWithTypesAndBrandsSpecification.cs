using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id == id)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
        //the base here is referring to the base specification class which takes the criteria / expression in its constructor!
    }
}

/*
Explanation of the cycle
1- In the product controller when we hit the httpget{id} endpoint, we create a new instance of this class (call it spec) and parse the ID
2- We then parse the expression into the base class which is simple BaseSpecification, hitting the ctor with parameters
3- In that construction, we take the expression as the criteria (in this case the ID check linq expression) - as well as including the other entities
So we now have both the expression and the include into this instance of this class
4- Then in the products controller we call for the generic repo method GetEntityWithSpecs and give it the instance of this class which was created in the same controller
5- The last method will call the ApplySpecification method also taking this spec object
6- We then run the specificationevaluator static class method "GetQuery" which takes the current entity and the spec object
7- The evaluator method adds a where clause to the query (as per the criteria / expression to update the entity as per the criteria)
and also aggregates all the includes into a list then returns the completely updated query (with the expression conditions and the included entities!)
8- the returned query or single object or list is then parsed into the controller and into the Ok status code!!! Hurray!
*/