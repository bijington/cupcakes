using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cupcakes.Customers.ViewModels;

public partial class CustomersPageViewModel : ObservableObject
{
    public CustomersPageViewModel(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    private readonly CustomerRepository _customerRepository;
    
    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];
    
    public async void OnNavigatedTo()
    {
        Customers = new ObservableCollection<Customer>(await _customerRepository.GetCustomers());
    }
}