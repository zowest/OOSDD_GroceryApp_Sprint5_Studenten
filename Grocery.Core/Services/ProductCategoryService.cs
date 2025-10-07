using System.Collections.Generic;
using System.Linq;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repository;
        public ProductCategoryService(IProductCategoryRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<int> GetProductsForCategory(int categoryId) => _repository.GetProductsForCategory(categoryId);
        public void AddProductToCategory(int categoryId, int productId) => _repository.Add(categoryId, productId);
        public void RemoveProductFromCategory(int categoryId, int productId) => _repository.Remove(categoryId, productId);
    }
}
