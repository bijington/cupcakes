using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.Customers.ViewModels;

[QueryProperty(nameof(Customer), "Customer")]
public partial class CustomerDetailsPageViewModel : ObservableObject
{
    public CustomerDetailsPageViewModel(
        ICustomerRepository customerRepository,
        IGeocoding geocoding,
        IMap map,
        IPhoneDialer phoneDialer)
    {
        _customerRepository = customerRepository;
        _geocoding = geocoding;
        _map = map;
        _phoneDialer = phoneDialer;
    }

    private readonly ICustomerRepository _customerRepository;
    private readonly IGeocoding _geocoding;
    private readonly IMap _map;
    private readonly IPhoneDialer _phoneDialer;

    public Customer Customer
    {
        set
        {
            Address = value.Address;
            CustomerName = value.Name;
            PhoneNumber = value.PhoneNumber;
        }
    }
    
    [ObservableProperty]
    private string _address = string.Empty;
    
    [ObservableProperty]
    private string _customerName = string.Empty;
    
    [ObservableProperty]
    private string _phoneNumber = string.Empty;

    [RelayCommand]
    private async Task OnShowMap()
    {
        if (string.IsNullOrWhiteSpace(Address))
        {
            return;
        }
        
        var locations = await _geocoding.GetLocationsAsync(Address);

        if (locations.Any() is false)
        {
            return;
        }
        
        await _map.OpenAsync(locations.First());
    }

    [RelayCommand]
    private void OnDialPhoneNumber()
    {
        if (_phoneDialer.IsSupported is false)
        {
            return;
        }
        
        _phoneDialer.Open(PhoneNumber);
    }
}