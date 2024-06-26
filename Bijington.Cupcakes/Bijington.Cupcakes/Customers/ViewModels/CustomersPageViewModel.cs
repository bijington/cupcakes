using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.Customers.ViewModels;

public partial class CustomersPageViewModel : ObservableObject
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersPageViewModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public ObservableCollection<Customer> Customers { get; } = [];
    
    [RelayCommand]
    async Task OnAddCustomer()
    {
        try
        {
            await Shell.Current.GoToAsync(RouteNames.AddCustomer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void OnNavigatedTo()
    {
        Task.Run(async () => await LoadCustomers());
    }
    
    async Task LoadCustomers()
    {
        var customers = await _customerRepository.GetCustomers();

        foreach (var customer in customers)
        {
            if (Customers.Any(p => p.Id == customer.Id))
            {
                continue;
            }
            
            Customers.Add(customer);
        }
    }
}