using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    
    [RelayCommand]
    private async Task OnAddCustomer()
    {
        await Shell.Current.GoToAsync(RouteNames.AddCustomer);
    }
    
    [RelayCommand]
    private async Task OnCustomerSelected(Customer customer)
    {
        await Shell.Current.GoToAsync(
            RouteNames.CustomerDetails,
            new Dictionary<string, object> { ["Customer"] = customer });
    }
}