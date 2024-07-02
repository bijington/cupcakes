# Part 3 - Navigation

In this part we are going to add 2 new pages into our application:

1. A page to create a new customer
1. A page to show a customers details

Each option above will take us through a slightly different approach to help us see what might fit future scenarios best.

We saw a little bit of Shell when we added the tabs into our application but we didn't do much with it. In this part we will look to use the URI based navigation to move between pages in our application.

## New customer page

Let's start by adding our new files:

* Add a new `ContentPage` to the `Customers/Pages` folder and call it `AddCustomerPage`
* Add a new `class` to the `Customers/ViewModels` folder and call it `AddCustomerPageViewModel`

We have now created our view and view model pair for the new screen to add a customer. Let's proceed to adding the functionality:

### 

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cupcakes.Customers.ViewModels;

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
```

###

```xaml
<?xml version="1.0" encoding="utf-8"?>

<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:Cupcakes.Customers.ViewModels"
             x:Class="Cupcakes.Customers.Pages.AddCustomerPage"
             Shell.PresentationMode="ModalAnimated"
             x:DataType="viewModels:AddCustomerPageViewModel">
    
    <VerticalStackLayout Margin="20" Spacing="10">
        <Label Text="Customer Name" />
        <Entry Text="{Binding CustomerName}" />
        
        <Label Text="Address" />
        <Editor Text="{Binding Address}" />
        
        <Label Text="Phone Number" />
        <Entry Text="{Binding PhoneNumber}" />
        
        <Button Text="Save" Command="{Binding SaveCommand}" />
    </VerticalStackLayout>
    
</ContentPage>
```

```csharp
using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers.Pages;

public partial class AddCustomerPage : ContentPage
{
    public AddCustomerPage(AddCustomerPageViewModel addCustomerPageViewModel)
    {
        InitializeComponent();
        BindingContext = addCustomerPageViewModel;
    }
}
```

The above should look pretty familiar to our `CustomersPage` class, note that we don't need to override the `OnNavigatedTo` method because we don't need to load any data for this page.
