using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using System.Collections.Generic;

namespace Grocery.Core.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        public IEnumerable<ProductCategory> GetAll()
        {
            return new List<ProductCategory>();
        }

        public IEnumerable<int> GetProductsForCategory(int categoryId)
        {
            return new List<int>();
        }
    }
}
