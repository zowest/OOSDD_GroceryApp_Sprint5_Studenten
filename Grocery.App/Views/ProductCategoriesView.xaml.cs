using Grocery.App.ViewModels;

namespace Grocery.App.Views;

public partial class ProductCategoriesView : ContentPage
{
    public ProductCategoriesView(ProductCategoriesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as ProductCategoriesViewModel)?.OnAppearing();
    }
}