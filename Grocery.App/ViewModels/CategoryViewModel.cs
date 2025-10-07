using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels;

public partial class CategoryViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;

    public ObservableCollection<Category> Categories { get; } = [];

    [ObservableProperty]
    private bool isBusy;

    public CategoryViewModel(ICategoryService categoryService)
    {
        Title = "Categorieën";
        _categoryService = categoryService;
        Load();
    }

    public override void Load()
    {
        Categories.Clear();
        foreach (var c in _categoryService.GetAll()) Categories.Add(c);
    }

    [RelayCommand]
    private async Task SelectCategory(Category category)
    {
        if (category == null) return;
        var navParams = new Dictionary<string, object> { { nameof(Category), category } };
        await Shell.Current.GoToAsync(nameof(Views.ProductCategoriesView), true, navParams);
    }
}