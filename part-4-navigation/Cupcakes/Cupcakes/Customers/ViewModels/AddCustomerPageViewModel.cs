using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cupcakes.Customers.ViewModels;

public partial class AddCustomerPageViewModel : ObservableObject
{
    public AddCustomerPageViewModel(
        CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    private readonly CustomerRepository _customerRepository;
    
    [ObservableProperty]
    private string _address = string.Empty;
    
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _customerName = string.Empty;
    
    [ObservableProperty]
    private string _phoneNumber = string.Empty;
    
    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task OnSave()
    {
        var customer = new Customer
        {
            Name = CustomerName,
            Address = Address,
            PhoneNumber = PhoneNumber
        };

        await _customerRepository.Save(customer);

        await Shell.Current.GoToAsync("..");
    }
    
    private bool CanSave() { return !string.IsNullOrWhiteSpace(CustomerName); }
}