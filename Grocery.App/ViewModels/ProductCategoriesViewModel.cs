using CommunityToolkit.Mvvm.ComponentModel;
using Grocery.Core.Models;
using Grocery.Core.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace Grocery.App.ViewModels;

[QueryProperty(nameof(Category), nameof(Category))]
public partial class ProductCategoriesViewModel : BaseViewModel
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly IProductService _productService;

    [ObservableProperty]
    private Category category = new(0, "");

    public ObservableCollection<Product> Products { get; } = new();

    public ProductCategoriesViewModel(IProductCategoryService productCategoryService, IProductService productService)
    {
        Title = "Producten";
        _productCategoryService = productCategoryService;
        _productService = productService;
    }

    partial void OnCategoryChanged(Category value)
    {
        Load();
    }

    public override void Load()
    {
        Products.Clear();
        if (Category is null || Category.Id == 0) return;

        Title = $"Producten - {Category.Name}";

        var productIds = (_productCategoryService.GetProductsForCategory(Category.Id) ?? Enumerable.Empty<int>())
                         .ToHashSet();

        var query = _productService.GetAll()
                                   .Where(p => p is not null);

        if (productIds.Count > 0)
            query = query.Where(p => productIds.Contains(p.Id));
        else
            query = query.Where(p => p.CategoryId == Category.Id);

        foreach (var p in query.OrderBy(p => p.Name))
            Products.Add(p);
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        if (Products.Count == 0 && Category is not null && Category.Id != 0)
            Load();
    }
}