using Bijington.Cupcakes.ViewModels;

namespace Bijington.Cupcakes.Pages;

public partial class AddProductPage : ContentPage
{
    public AddProductPage(AddProductViewModel addProductViewModel)
    {
        InitializeComponent();
        BindingContext = addProductViewModel;
    }
}