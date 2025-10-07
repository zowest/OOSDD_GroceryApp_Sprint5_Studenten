using Grocery.Core.Interfaces.Repositories;

namespace Grocery.Core.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        // Made static so links persist across page navigations
        private static readonly Dictionary<int, List<int>> productCategory = new();

        public void Add(int categoryId, int productId)
        {
            if (!productCategory.TryGetValue(categoryId, out var list))
            {
                list = new List<int>();
                productCategory[categoryId] = list;
            }
            if (!list.Contains(productId))
                list.Add(productId);
        }

        public IEnumerable<int> GetProductsForCategory(int categoryId)
        {
            return productCategory.TryGetValue(categoryId, out var list) ? list : Enumerable.Empty<int>();
        }

        public void Remove(int categoryId, int productId)
        {
            if (productCategory.TryGetValue(categoryId, out var list))
            {
                list.Remove(productId);
            }
        }
    }
}