using System.Collections.ObjectModel;
using Bijington.Cupcakes.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.Orders.ViewModels;

public partial class OrdersPageViewModel : ObservableObject
{
    public OrdersPageViewModel(IOrderRepository orderRepository, ISettingsRepository settingsRepository)
    {
        _orderRepository = orderRepository;
        _settingsRepository = settingsRepository;
    }
    
    [ObservableProperty]
    private string _currency;
    
    private readonly IOrderRepository _orderRepository;
    private readonly ISettingsRepository _settingsRepository;

    [ObservableProperty]
    private IList<OrderDate> _orders;
    
    [RelayCommand]
    private async Task OnAddOrder()
    {
        try
        {
            await Shell.Current.GoToAsync(RouteNames.AddOrder);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async void OnNavigatedTo()
    {
        await LoadOrders();
    }

    private async Task LoadOrders()
    {
        Currency = _settingsRepository.Currency;
        var orders = await _orderRepository.GetOrders();

        var newList = new List<OrderDate>();

        foreach (var grouping in orders.GroupBy(o => o.Date, o => o))
        {
            newList.Add(new OrderDate(DateOnly.FromDateTime(grouping.Key), grouping.ToList()));
        }

        Orders = newList;
    }
}