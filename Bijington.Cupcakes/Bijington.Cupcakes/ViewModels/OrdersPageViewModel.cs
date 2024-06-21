using System.Collections.ObjectModel;
using Bijington.Cupcakes.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bijington.Cupcakes.ViewModels;

public partial class OrdersPageViewModel : ObservableObject
{
    public ObservableCollection<Order> Orders { get; } = [];
}