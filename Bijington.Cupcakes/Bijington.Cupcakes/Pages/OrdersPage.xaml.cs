using Bijington.Cupcakes.ViewModels;

namespace Bijington.Cupcakes.Pages;

public partial class OrdersPage : ContentPage
{
    public OrdersPage(OrdersPageViewModel ordersPageViewModel)
    {
        InitializeComponent();
        BindingContext = ordersPageViewModel;
    }
}