using System;
using System.Collections.Generic;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public IEnumerable<int> GetProductsForCategory(int categoryId)
        {
            var _categoryProducts = new Dictionary<int, List<int>>
            {
                { 1, new List<int>{ 10, 11 } },
                { 2, new List<int>{ 20 } }
            };

            if (_categoryProducts.TryGetValue(categoryId, out var ids))
                return ids;
            return Enumerable.Empty<int>();
        }
    }
}
