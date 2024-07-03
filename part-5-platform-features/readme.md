# Part 5 - Platform features

In part 5 we will discover some of the great [platform abstractions](https://learn.microsoft.com/dotnet/maui/platform-integration) that are provided to us out of the box in .NET MAUI. The last part saw us adding in the page to view a customers details, this shows both their address and phone number. We can use the following 3 features from .NET MAUI to improve a users experience:

1. [Geocoding](https://learn.microsoft.com/dotnet/maui/platform-integration/device/geocoding) to find the customers longitude and latitude
1. [Map](https://learn.microsoft.com/dotnet/maui/platform-integration/appmodel/maps) to show the customers location
1. [PhoneDialer](https://learn.microsoft.com/dotnet/maui/platform-integration/communication/phone-dialer) to provide a quick way to call a customer

Let's begin by registering these services.

## Register our dependencies

Inside our `AddCustomers` method in the `ServiceCollectionExtensions` class we can add the following lines:

```csharp
serviceCollection.AddSingleton(Geocoding.Default);
serviceCollection.AddSingleton(Map.Default);
serviceCollection.AddSingleton(PhoneDialer.Default);
```

.NET MAUI provides us with a singleton instance for these features accessed through the `Default` property. We want to make use of the dependency injection layer which is why we are registering them as above.

## Add the dependencies to our `CustomerDetailsPageViewModel`

We can add the following constructor and backing fields to the `CustomerDetailsPageViewModel` file:

```csharp
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
```

## Using our new dependencies

We can add the following 2 methods that will generate commands that we can bind to our view:

```csharp
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
```

## Final `CustomerDetailsPageViewModel` code

Our `CustomerDetailsPageViewModel` should look as follows once we have finished:

```csharp
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
```

## Connecting our view

Inside the `CustomerDetailsPage.xaml` file we can make the following changes:

Add the following `Button` below the `Label` bound to the `Address` property:

```xaml
<Button Text="Show on map" Command="{Binding ShowMapCommand}" />
```

Modify this line:

```xaml
<Label Text="{Binding PhoneNumber}">
```

to the following:

```xaml
<Label Text="{Binding PhoneNumber}">
    <Label.GestureRecognizers>
        <TapGestureRecognizer Command="{Binding DialPhoneNumberCommand}" />
    </Label.GestureRecognizers>
</Label>
```

Hopefully the above changes should start to feel familiar.

## Final `CustomerDetailPage` code

The `CustomerDetailPage.xaml` file should look as follows once we have finished:

```xaml
<?xml version="1.0" encoding="utf-8"?>

<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:Bijington.Cupcakes.Customers.ViewModels"
             x:Class="Bijington.Cupcakes.Customers.Pages.CustomerDetailsPage"
             x:DataType="viewModels:CustomerDetailsPageViewModel">
    
    <VerticalStackLayout Margin="20" Spacing="10">
        <Label Text="Customer Name" />
        <Label Text="{Binding CustomerName}" />
        
        <Label Text="Address" />
        <Label Text="{Binding Address}" />
        <Button Text="Show on map" Command="{Binding ShowMapCommand}" />

        <Label Text="Phone Number" />
        <Label Text="{Binding PhoneNumber}">
            <Label.GestureRecognizers>
                <TapGestureRecognizer Command="{Binding DialPhoneNumberCommand}" />
            </Label.GestureRecognizers>
        </Label>
    </VerticalStackLayout>
    
</ContentPage>
```

## Run our application

We can now run our application and see that once we have selected a customer we can click on the Show on map `Button` which will open the default map application on the device.

We can also tap on the phone number and it will start a call to that number. Note this functionality doesn't work on simulators/emulators and will crash if we don't have the `if (_phoneDialer.IsSupported is false)` check.
