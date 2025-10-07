using Grocery.Core.Models;
using System.Collections.Generic;

namespace Grocery.Core.Interfaces.Services
{
    public interface IProductCategoryService 
    {
        IEnumerable<int> GetProductsForCategory(int categoryId);
    }
}
