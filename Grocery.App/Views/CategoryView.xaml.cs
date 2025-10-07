using Grocery.App.ViewModels;

namespace Grocery.App.Views;

public partial class CategoryView : ContentPage
{
    public CategoryView(CategoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as CategoryViewModel)?.OnAppearing();
    }
}