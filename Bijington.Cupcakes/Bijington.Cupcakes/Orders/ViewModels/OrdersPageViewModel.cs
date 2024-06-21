using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.Orders.ViewModels;

public partial class OrdersPageViewModel : ObservableObject
{
    public ObservableCollection<Order> Orders { get; } = [];
    
    [RelayCommand]
    void OnAddOrder()
    {
        _ = Shell.Current.GoToAsync(RouteNames.AddOrder);
    }
}