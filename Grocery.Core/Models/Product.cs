using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class Product : Model
    {
        [ObservableProperty]
        public int stock;
        public DateOnly ShelfLife { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public Product(int id, string name, int stock, int categoryId, double price) : this(id, name, stock, categoryId, default, price) { }

        public Product(int id, string name, int stock, int categoryId, DateOnly shelfLife, double price) : base(id, name)
        {
            Stock = stock;
            CategoryId = categoryId;
            ShelfLife = shelfLife;
            Price = price;
        }
        public override string? ToString()
        {
            return $"{Name} - {Stock} op voorraad";
        }
    }
}
