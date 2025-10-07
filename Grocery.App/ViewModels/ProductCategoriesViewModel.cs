using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Models;
using Grocery.Core.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Grocery.App.ViewModels;

[QueryProperty(nameof(Category), nameof(Category))]
public partial class ProductCategoriesViewModel : BaseViewModel
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly IProductService _productService;

    [ObservableProperty]
    private Category category = new(0, "");

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private string statusMessage = string.Empty;

    // Left side: products already linked to the category (with Id + Name)
    public ObservableCollection<Product> CategoryProducts { get; } = new();

    // Right side: all products (unfiltered master list)
    public ObservableCollection<Product> AllProducts { get; } = new();

    // Right side: filtered view (SearchBar drives this)
    public ObservableCollection<Product> FilteredProducts { get; } = new();

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

    partial void OnSearchTextChanged(string value)
    {
        UpdateFilteredProducts();
    }

    [RelayCommand]
    private void ClearSearch()
    {
        if (!string.IsNullOrWhiteSpace(SearchText))
            SearchText = string.Empty;
    }

    public override void Load()
    {
        CategoryProducts.Clear();
        AllProducts.Clear();
        FilteredProducts.Clear();
        StatusMessage = string.Empty;

        if (Category is null || Category.Id == 0)
        {
            StatusMessage = "Geen categorie geselecteerd.";
            return;
        }

        Title = $"Producten - {Category.Name}";

        var all = _productService.GetAll() ?? [];
        foreach (var p in all.OrderBy(p => p.Name))
            AllProducts.Add(p);

        var linkedIds = (_productCategoryService.GetProductsForCategory(Category.Id) ?? Enumerable.Empty<int>()).ToHashSet();

        foreach (var p in AllProducts.Where(p => linkedIds.Contains(p.Id)))
            CategoryProducts.Add(p);

        UpdateFilteredProducts();

        if (CategoryProducts.Count == 0)
            StatusMessage = "Nog geen producten gekoppeld.";
    }

    private void UpdateFilteredProducts()
    {
        FilteredProducts.Clear();
        var term = SearchText?.Trim();
        var linkedIds = CategoryProducts.Select(p => p.Id).ToHashSet(); // exclude already linked
        IEnumerable<Product> source = AllProducts.Where(p => !linkedIds.Contains(p.Id));
        if (!string.IsNullOrWhiteSpace(term))
            source = source.Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase));

        foreach (var p in source.OrderBy(p => p.Name))
            FilteredProducts.Add(p);
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        if (AllProducts.Count == 0 && Category is not null && Category.Id != 0)
            Load();
    }

    [RelayCommand]
    private void AddProduct(Product? product)
    {
        if (product is null || Category is null || Category.Id == 0) return;
        if (CategoryProducts.Any(p => p.Id == product.Id)) return;
        _productCategoryService.AddProductToCategory(Category.Id, product.Id);
        CategoryProducts.Add(product);
        FilteredProducts.Remove(product);
        StatusMessage = string.Empty;
    }
}