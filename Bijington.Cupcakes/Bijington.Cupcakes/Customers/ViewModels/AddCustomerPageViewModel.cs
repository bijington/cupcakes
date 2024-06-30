using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.Customers.ViewModels;

public partial class AddCustomerPageViewModel : ObservableObject
{
    public AddCustomerPageViewModel(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    private readonly ICustomerRepository _customerRepository;
    
    [ObservableProperty]
    private string _address;
    
    [ObservableProperty]
    private string _customerName;
    
    [ObservableProperty]
    private string _phoneNumber;
    
    [RelayCommand]
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
}