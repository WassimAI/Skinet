using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersWithCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersWithCountSpecification(ProductSpecParams productParams)
        : base(x=>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && //either one of the statements happen here depending on the status of the first (true of false)
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            
        }
    }
}