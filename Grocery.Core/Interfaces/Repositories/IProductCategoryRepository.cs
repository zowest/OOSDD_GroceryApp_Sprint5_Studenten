using System.Collections.Generic;
using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<int> GetProductsForCategory(int categoryId);
        void Add(int categoryId, int productId);
        void Remove(int categoryId, int productId);
    }
}
