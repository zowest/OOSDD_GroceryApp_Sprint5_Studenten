using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = new()
        {
            new Category(1, "Groenten"),
            new Category(2, "Fruit"),
            new Category(3, "Dranken"),
            new Category(4, "Snacks")
        };

        public IEnumerable<Category> GetAll() => _categories;

        public Category? Get(int id) => _categories.FirstOrDefault(c => c.Id == id);

        public Category Add(Category item)
        {
            var nextId = _categories.Count == 0 ? 1 : _categories.Max(c => c.Id) + 1;
            item.Id = nextId;
            _categories.Add(item);
            return item;
        }

        public Category? Update(Category item)
        {
            var existing = Get(item.Id);
            if (existing == null) return null;
            existing.Name = item.Name;
            return existing;
        }

        public Category? Delete(Category item)
        {
            if (_categories.Remove(item)) return item;
            return null;
        }
    }
}
