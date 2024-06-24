using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace Bijington.Cupcakes.Orders.ViewModels;

public partial class OrdersPageViewModel : ObservableObject
{
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
}