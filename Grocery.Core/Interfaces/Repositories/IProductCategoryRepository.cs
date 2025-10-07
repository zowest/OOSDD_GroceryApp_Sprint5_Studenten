using System.Collections.Generic;
using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<int> GetProductsForCategory(int categoryId);
    }
}
