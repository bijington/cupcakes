using Bijington.Cupcakes.ViewModels;

namespace Bijington.Cupcakes.Pages;

public partial class ProductsPage : ContentPage
{
    public ProductsPage(ProductsPageViewModel productsPageViewModel)
    {
        InitializeComponent();
        BindingContext = productsPageViewModel;
    }
}