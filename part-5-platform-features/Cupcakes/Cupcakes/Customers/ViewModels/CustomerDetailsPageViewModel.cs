using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cupcakes.Customers.ViewModels;

[QueryProperty(nameof(Customer), "Customer")]
public partial class CustomerDetailsPageViewModel : ObservableObject
{
    public CustomerDetailsPageViewModel(
        IGeocoding geocoding,
        IMap map,
        IPhoneDialer phoneDialer)
    {
        _geocoding = geocoding;
        _map = map;
        _phoneDialer = phoneDialer;
    }

    private readonly IGeocoding _geocoding;
    private readonly IMap _map;
    private readonly IPhoneDialer _phoneDialer;
    
    [ObservableProperty]
    private Customer? _customer;
    
    [RelayCommand]
    private async Task OnShowMap()
    {
        if (string.IsNullOrWhiteSpace(Customer.Address))
        {
            return;
        }
    
        var locations = await _geocoding.GetLocationsAsync(Customer.Address);

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
    
        _phoneDialer.Open(Customer.PhoneNumber);
    }
}