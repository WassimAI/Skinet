using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
         Task<Product> GetProductByIdAsync(int id);
         Task<IReadOnlyList<Product>> GetProductsAsync(); //IReadOnly means we can only read with no other functionalities coming with normal list
         Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
         Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}