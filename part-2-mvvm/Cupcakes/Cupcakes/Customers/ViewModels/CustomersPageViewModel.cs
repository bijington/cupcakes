using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cupcakes.Customers.ViewModels;

public partial class CustomersPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Customer> _customers;
    
    public void OnNavigatedTo()
    {
        Customers =
        [
            new Customer { Name = "Cookie Monster", Address = "1 Sesame Street" },
            new Customer { Name = "Sherlock Holmes", Address = "221B, Baker Street, London"}
        ];
    }
}