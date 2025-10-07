using System.Collections.Generic;
using System.Linq;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;

namespace Grocery.Core.Services
{
    public class ProductCategoryService(IProductCategoryRepository productCategoryRepository) : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

        public IEnumerable<int> GetProductsForCategory(int categoryId)
        {
            return _productCategoryRepository.GetAll()
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.ProductId);
        }
    }
}
