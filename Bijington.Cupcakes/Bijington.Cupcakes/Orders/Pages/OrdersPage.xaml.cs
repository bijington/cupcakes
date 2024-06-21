using Bijington.Cupcakes.Orders.ViewModels;

namespace Bijington.Cupcakes.Orders.Pages;

public partial class OrdersPage : ContentPage
{
    public OrdersPage(OrdersPageViewModel ordersPageViewModel)
    {
        InitializeComponent();
        BindingContext = ordersPageViewModel;
    }
}