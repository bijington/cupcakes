using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace Bijington.Cupcakes.Orders.ViewModels;

public partial class OrdersPageViewModel : ObservableObject
{
    private readonly IOrderRepository _orderRepository;

    public OrdersPageViewModel(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public ObservableCollection<Order> Orders { get; } = [];
    
    [RelayCommand]
    async Task OnAddOrder()
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
    
    public void OnNavigatedTo()
    {
        Task.Run(async () => await LoadOrders());
    }
    
    async Task LoadOrders()
    {
        var orders = await _orderRepository.GetOrders();

        foreach (var order in orders)
        {
            if (Orders.Any(p => p.Id == order.Id))
            {
                continue;
            }
            
            Orders.Add(order);
        }
    }
}