# Part 4 - Navigation

In this part we are going to add 2 new pages into our application:

1. A page to create a new customer
1. A page to show a customers details

Each option above will take us through a slightly different approach to help us see what might fit future scenarios best.

We saw a little bit of Shell when we added the tabs into our application but we didn't do much with it. In this part we will look to use the [URI based navigation](https://learn.microsoft.com/dotnet/maui/fundamentals/shell/navigation) to move between pages in our application.

## New customer page

Let's start by adding our new files:

* Add a new `ContentPage` to the `Customers/Pages` folder and call it `AddCustomerPage`
* Add a new `class` to the `Customers/ViewModels` folder and call it `AddCustomerPageViewModel`

We have now created our view and view model pair for the new screen to add a customer. Let's proceed to adding the functionality:

### Add functionality to our `AddCustomerPageViewModel`

Let's open the `AddCustomerPageViewModel` file and apply the following changes:

#### Add using statements

We can add there at the top of the file.

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
```

#### Add a dependency to CustomerRepository

We do this by adding a parameter to the constructor and storing the value in a backing field for future use.

```csharp
public AddCustomerPageViewModel(
    CustomerRepository customerRepository)
{
    _customerRepository = customerRepository;
}

private readonly CustomerRepository _customerRepository;
```

#### Add properties to bind to our view

Making full use of the `CommunityToolkit.Mvvm` like we did in part 2.

```csharp
[ObservableProperty]
private string _address = string.Empty;

[ObservableProperty]
private string _customerName = string.Empty;

[ObservableProperty]
private string _phoneNumber = string.Empty;
```

#### Add a command to handle the save button click/tap

This is our first introduction to using commands so let's see how to do it the long winded way and then see how the `CommunityToolkit.Mvvm` can help to simplify it for us.

To expose a command we need to add a property, assign a value to that property that points to an `Action` or method that provides the implementation:

```csharp
public class AddCustomerPageViewModel
{
    public ICommand SaveCommand { get; }

    public AddCustomerPageViewModel()
    {
        SaveCommand = new Command(OnSave);
    }

    private void OnSave()
    {
    }
}
```

This would result in the `OnSave` method being called when a `Button` is clicked, when the `Command` property of the `Button` is bound to our `SaveCommand` property. This doesn't look like a lot of code but it can grow quickly if we have a lot of commands, thankfully the `CommunityToolkit.Mvvm` can help here through the use of the `RelayCommand` attribute.

We can add the `RelayCommand` attribute to our `OnSave` method and delete the rest of the code from above, leaving us with:

```csharp
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
```

Much like how we apply the `ObservableProperty` to a field for the toolkit to auto-generate some code for our command. The act of adding the `RelayCommand` on the `OnSave` method will result in the toolkit generating a property called `SaveCommand`.

We should consider that the current code above will allow customers with no details to be saved, this is unlikely to be desired functionality, we should add in some protection for this.

#### Guarding against empty values

Commands in .NET MAUI execute some functionality when they are invoked (button click, etc.) but they also expose a way to define when they can be executed. The `RelayCommand` also allows us to control this.

First we should change the `OnSave` method to look as follows:

```csharp
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
```

This adds a parameter into the `RelayCommand` attribute and also adds a new method called `CanSave`. Currently we say that a customer can be saved if a name has been supplied. Of course we could change this as we see fit.

Next we also need to update the `_customerName` field to inform the toolkit that we will notify that the status of whether the `SaveCommand` can be executed when the `CustomerName` property changes since we are using that property to decide whether we can save.

```csharp
[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveCommand))]
private string _customerName;
```

#### Final code

The resulting `AddCustomerPageViewModel` should look as follows:

```csharp
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
```

Explain value in the return type of `Task` here too.

Notice that after saving our new customer we then request that Shell navigates to `".."` this means that it will navigate one level up in the navigation stack - basically it will hide the current page.

### Update the `AddCustomerPage.xaml` file

We can change the code in our `AddCustomerPage.xaml` file. The below structure will result in a vertical layout showing each control in order that they are defined.

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

One new detail that we have added to our view is `Shell.PresentationMode="ModalAnimated"` this means that when our `AddCustomerPage` is presented it will be presented as a model page rather than being navigated to. The main difference here is that there won't be a back button or a title bar presented.

### Update the `AddCustomerPage.xaml.cs` file



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

## Showing the new customer page

There are a number of steps that need to be followed in order to show our new page:

### Registering routes

As we briefly touched on earlier Shell navigation is URI based, this means that we need to register the route before we can use it. We can do this through the `Routing.RegisterRoute();` method, it allows us to provide a route name (the part of the URI we pass to Shell) and a type (this is the type of our page `AddCustomerPage`). I strongly recommend that we avoid using the same string in multiple places, for this reason I would like to show off a slightly confusing initial approach but hopefully it will make some sense. Let's do this and see how it can benefit us:

#### Create a new RouteNames class

Add a new file at the root of our project called `RouteNames`. We just need to make one change to the file which will be to make it `partial`:

```csharp
namespace Cupcakes;

public partial class RouteNames
{
    
}
```

#### Create a second RouteNames class

Yes that is correct! We are going to add the same class name but inside the `Customers` folder. This is where the value of the `partial` keyword comes in:

```csharp
using Cupcakes.Customers.Pages;

namespace Cupcakes;

public partial class RouteNames
{
    public const string AddCustomer = nameof(AddCustomerPage);
}
```

This allows us to have a single `RouteNames` class that can be accessed within our application, we can register the route name using the `AddCustomer` property name and we can also use it to navigate to the page.

#### Actually register the route

Inside the `AddCustomers` method in our `ServiceCollectionExtensions` class we can add the following registration:

```csharp
serviceCollection.AddTransient<AddCustomerPage>();
serviceCollection.AddTransient<AddCustomerPageViewModel>();
Routing.RegisterRoute(RouteNames.AddCustomer, typeof(AddCustomerPage));
```

We can make use of the `CommunityToolkit.Maui` package to simplify the above registration

#### Include the `CommunityToolkit.Maui` NuGet package

We first need to add the `CommunityToolkit.Maui` package to the project via NuGet:

* Right click on the `Cupcakes` project
* Select Manage NuGet Packages...
* Type `CommunityToolkit.Maui` in the search bar
* Select `CommunityToolkit.Maui` result
* Click install

This package will now be installed and ready for us to use although we need to apply a change to our `MauiProgram.cs` file:

```csharp
var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
```

Needs to change to the following:

```csharp
var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseMauiCommunityToolkit()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
```

Notice the addition of the `.UseMauiCommunityToolkit()` method. Note this is a common pattern that you will see when bringing in libraries to your .NET MAUI applications.

#### Simplified registration

We can now register our page and view model as follows:

```csharp
serviceCollection.AddTransientWithShellRoute<AddCustomerPage, AddCustomerPageViewModel>(RouteNames.AddCustomer);
```

### Adding a button to trigger showing our new page

We can add the following entry to our `CustomersPage.xaml` file, above the `<CollectionView>` element:

```xaml
<ContentPage.ToolbarItems>
    <ToolbarItem 
        Text="Add"
        Command="{Binding AddCustomerCommand}" />
</ContentPage.ToolbarItems>
```

This will add a new button into the title bar and bind it to a new method that we will now create.

### Adding the `OnAddCustomer` method

Inside our `CustomersPageViewModel` we can add the following method:

```csharp
[RelayCommand]
private async Task OnAddCustomer()
{
    await Shell.Current.GoToAsync(RouteNames.AddCustomer);
}
```

This now requests that Shell will navigate to a URI and that URI we pass in is our common RouteName that we created earlier.

### Testing our new page

If we run our application, we will see that there is an Add button showing in the title bar, if we click on that it will show our new add customer page. If we proceed to filling in some details and click Save then we will see that the add page closes and the newly added customer is visible in the list of customers.

You will also notice that the Save button is disabled initially and only becomes enabled once you type into the name entry.

## Customer details page

We are going to perform a very similar set of steps to the [previous section](#new-customer-page) and even the UI will look familiar, we are making a separate page to create a clear distinction between creating data and viewing data. Plus it means that we can get some exposure to some other goodies that .NET MAUI offers.

Let's start by adding our new files:

* Add a new `ContentPage` to the `Customers/Pages` folder and call it `CustomerDetailsPage`
* Add a new `class` to the `Customers/ViewModels` folder and call it `CustomerDetailsPageViewModel`

We have now created our view and view model pair for the new screen to add a customer. Let's proceed to adding the functionality:

### Add functionality to our `CustomerDetailsPageViewModel`

Let's open the `CustomerDetailsPageViewModel` file and apply the following changes:

#### Add using statements to `CustomerDetailsPageViewModel`

We can add there at the top of the file.

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
```

In truth we don't need to add these upfront because the tooling does a good job of providing a quick way to do it but I do like to draw emphasis to where the value is coming from.

#### Add properties to `CustomerDetailsPageViewModel`

Making full use of the `CommunityToolkit.Mvvm` like we did in part 2.

```csharp
[ObservableProperty]
private Customer? _customer;
```

#### Final code for `CustomerDetailsPageViewModel`

The resulting `CustomerDetailsPageViewModel` should look as follows:

```csharp
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cupcakes.Customers.ViewModels;

[QueryProperty(nameof(Customer), "Customer")]
public partial class CustomerDetailsPageViewModel : ObservableObject
{
    [ObservableProperty]
    private Customer? _customer;
}
```

### Update the `CustomerDetailsPage.xaml` file

We can change the code in our `CustomerDetailsPage.xaml` file. The below structure will result in a vertical layout showing each control in order that they are defined.

```xaml
<?xml version="1.0" encoding="utf-8"?>

<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:Cupcakes.Customers.ViewModels"
             x:Class="Cupcakes.Customers.Pages.CustomerDetailsPage"
             x:DataType="viewModels:CustomerDetailsPageViewModel">
    
    <VerticalStackLayout Margin="20" Spacing="10">
        <Label Text="Customer Name" />
        <Label Text="{Binding Customer.Name}" />
        
        <Label Text="Address" />
        <Label Text="{Binding Customer.Address}" />

        <Label Text="Phone Number" />
        <Label Text="{Binding Customer.PhoneNumber}" />
    </VerticalStackLayout>
    
</ContentPage>
```

We should notice the following things:

* We are not setting the `Shell.PresentationMode` which means it will use the default of `Animated` and therefore it will be added to the navigation stack and present a back button
* We are binding to a sub property of the `Customer` class
* If the `Customer` property is `null` then we won't actually experience any crashes or exceptions unlike with C#. That being said we can add in a `TargetNullValue` in order to handle this scenario and present a different value.

### Update the `CustomerDetailsPage.xaml.cs` file

```csharp
using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers.Pages;

public partial class CustomerDetailsPage : ContentPage
{
    public CustomerDetailsPage(CustomerDetailsPageViewModel customerDetailsPageViewModel)
    {
        InitializeComponent();
        BindingContext = customerDetailsPageViewModel;
    }
}
```

The above should look pretty familiar to our `AddCustomer` class.

## Showing the customer details page

There are a number of steps that need to be followed in order to show our new page:

### Registering a route to `CustomerDetailsPage`

We can add the following line into our `RouteNames` class file under the `Customers` folder:

```csharp
public const string EditCustomer = nameof(CustomerDetailsPage);
```

Leaving us with a file that will look as follows:

```csharp
using Cupcakes.Customers.Pages;

namespace Cupcakes;

public partial class RouteNames
{
    public const string AddCustomer = nameof(AddCustomerPage);

    public const string CustomerDetails = nameof(CustomerDetailsPage);
}
```

We can now register the route and pages just as we did for the `AddCustomerPage` above, inside the `AddCustomers` method in our `ServiceCollectionExtensions` class we can add the following registration:

```csharp
serviceCollection.AddTransientWithShellRoute<CustomerDetailsPage, CustomerDetailsPageViewModel>(RouteNames.CustomerDetails);
```

### Showing our new page

There are a number of ways that we could achieve this, for example we could handle the selection changing from the `CollectionView` but we don't really want to show selection, in fact we just want the user to tap or click on an item in the list to view the details, for this we can handle this sort of interaction with a `GestureRecognizer` and specifically the `TapGestureRecognizer`.

If we open the `CustomersPage.xaml` file and add the following inside the `<Border>` element:

```xaml
<Border.GestureRecognizers>
    <TapGestureRecognizer 
        Command="{Binding CustomerSelectedCommand, Source={RelativeSource AncestorType={x:Type customerViewModels:CustomersPageViewModel}}}" 
        CommandParameter="{Binding}" />
</Border.GestureRecognizers>
```

We covered in [Part 2 - MVVM](../part-2-mvvm/readme.md) that bindings we create are against the `BindingContext` this is a useful default approach but sometimes we need to bind to something outside of the current context. In our example we want to handle the tap interaction on each individual item (`Customer`) from our list (`CollectionView`) but we want to handle the interaction in our `CustomersPageViewModel`. To do this we can make use of [Relative bindings](https://learn.microsoft.com/dotnet/maui/fundamentals/data-binding/relative-bindings#bind-to-an-ancestor).

There are a number of options when binding to something relative to the current context but the approach that we are going to use is the `AncestorType` option. This involves the binding engine walking up the visual tree until it can find the type that we have provided, in our case this is the `CustomersPageViewModel`.

Additionally we are setting the `CommandParameter` and just using the value of `{Binding}`, this means two things:

* When the `CustomerSelectedCommand` is executed it will be passed in a value
* That value will be the binding context of the `Border` that the `TapGestureRecognizer` is attached to, which is the `Customer`

### Adding the `OnCustomerSelected` method

Inside our `CustomersPageViewModel` we can add the following method:

```csharp
[RelayCommand]
private async Task OnCustomerSelected(Customer customer)
{
    await Shell.Current.GoToAsync(
        RouteNames.CustomerDetails,
        new Dictionary<string, object> { ["Customer"] = customer });
}
```

This now requests that Shell will navigate to a URI and that URI we pass in is our common RouteName that we created earlier, we also pass in the dictionary which results in parameters passed into the query string, this then allows us to receive that value in the view or view model when Shell navigates to our page. How the value gets mapped is tied into this line ```csharp[QueryProperty(nameof(Customer), "Customer")]` ``that we discussed earlier.

## Finishing up

If we run our application we will be able to add customers to our application and also tap to view their details. Let's proceed onto [Part 5](../part-5-platform-features/readme.md) which will allow us to add some extra functionality into our details page to provide a more interactive experience for our users.
