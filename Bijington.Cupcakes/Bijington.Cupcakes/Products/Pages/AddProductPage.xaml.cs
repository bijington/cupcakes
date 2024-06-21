using Bijington.Cupcakes.Products.ViewModels;

namespace Bijington.Cupcakes.Products.Pages;

public partial class AddProductPage : ContentPage
{
    public AddProductPage(AddProductPageViewModel addProductPageViewModel)
    {
        InitializeComponent();
        BindingContext = addProductPageViewModel;
    }
}