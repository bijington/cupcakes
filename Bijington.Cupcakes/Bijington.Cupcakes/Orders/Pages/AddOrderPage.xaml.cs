using Bijington.Cupcakes.Orders.ViewModels;

namespace Bijington.Cupcakes.Orders.Pages;

public partial class AddOrderPage : ContentPage
{
    public AddOrderPage(AddOrderPageViewModel addOrderPageViewModel)
    {
        InitializeComponent();
        BindingContext = addOrderPageViewModel;
    }
}