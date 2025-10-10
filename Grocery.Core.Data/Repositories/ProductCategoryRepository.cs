using Grocery.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Grocery.Core.Data.Repositories // keep data namespace
{
    // Renamed class to avoid clash with similarly named class in Grocery.Core assembly
    public class ProductCategoryLinkRepository : IProductCategoryRepository
    {
        private readonly Dictionary<int, List<int>> _productCategory = new();
        public void Add(int categoryId, int productId)
        {
            if (!_productCategory.TryGetValue(categoryId, out var list))
            {
                list = new List<int>();
                _productCategory[categoryId] = list;
            }
            if (!list.Contains(productId))
                list.Add(productId);
        }

        public IEnumerable<int> GetProductsForCategory(int categoryId)
        {
            return _productCategory.TryGetValue(categoryId, out var list) ? list : Enumerable.Empty<int>();
        }

        public void Remove(int categoryId, int productId)
        {
            if (_productCategory.TryGetValue(categoryId, out var list))
            {
                list.Remove(productId);
            }
        }
    }
}
