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
    private Category category = new Category(0, "");

    public ObservableCollection<Product> Products { get; } = [];

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
        if (Category is null) return;

        var productIds = _productCategoryService.GetProductsForCategory(Category.Id)
                                                .ToHashSet();

        var productsInCategory = _productService.GetAll()
            .Where(p => p is not null && productIds.Contains(p.Id));

        foreach (var p in productsInCategory)
        {
            Products.Add(p);
        }
    }
}