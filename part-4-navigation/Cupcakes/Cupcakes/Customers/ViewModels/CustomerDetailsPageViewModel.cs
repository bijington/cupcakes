using CommunityToolkit.Mvvm.ComponentModel;

namespace Cupcakes.Customers.ViewModels;

[QueryProperty(nameof(Customer), "Customer")]
public partial class CustomerDetailsPageViewModel : ObservableObject
{
    [ObservableProperty]
    private Customer? _customer;
}