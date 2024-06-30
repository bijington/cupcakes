using Bijington.Cupcakes.Orders.ViewModels;

namespace Bijington.Cupcakes.Orders.Pages;

public partial class OrdersPage : ContentPage
{
    public OrdersPage(OrdersPageViewModel ordersPageViewModel)
    {
        InitializeComponent();
        _ordersPageViewModel = ordersPageViewModel;
        BindingContext = ordersPageViewModel;
    }
    
    private readonly OrdersPageViewModel _ordersPageViewModel;

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        
        _ordersPageViewModel.OnNavigatedTo();
    }
}