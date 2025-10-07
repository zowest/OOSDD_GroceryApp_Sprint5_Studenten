using Grocery.Core.Models;
using System.Collections.Generic;

namespace Grocery.Core.Interfaces.Services
{
    public interface IProductCategoryService 
    {
        IEnumerable<int> GetProductsForCategory(int categoryId);
        // FR2: koppel product aan categorie
        void AddProductToCategory(int categoryId, int productId);
        void RemoveProductFromCategory(int categoryId, int productId);
    }
}
