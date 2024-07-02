# Part 2 - Architecture

We will be making use of the MVVM pattern which is widely used in .NET MAUI applications.

Model View ViewModel is a software design pattern that focuses on separating the user interface (View) from the business logic (Model), it achieves this with the use of a layer in between (ViewModel). MVVM allows a clean separation of presentation and business logic.

```mermaid
flowchart LR
    View([View]) <--Data binding-->
    ViewModel([View Model]) -->
    Model([Model])
    Model -.-> ViewModel
```

## Setup

As with other parts we need to perform a little bit of work in order to prepare our application for this part.

Let's make the following changes:

* Inside the `Customers` folder create a new folder and call it `ViewModels`.
* Add a new `Class` inside the `ViewModels` folder and call it `CustomersPageViewModel`.

## Understanding INotifyPropertyChanged

There is a key interface that we need to implement in our view model classes that provides the ability to bind data to/from the views within our application. This interface is called `INotifyPropertyChanged`. Let's proceed to adding it into our new `CustomersPageViewModel` class.

### Implementing INotifyPropertyChanged

1. Open the `CustomersPageViewModel.cs` file.
2. In `CustomersPageViewModel.cs`, implement INotifyPropertyChanged by changing this

```csharp
public class CustomersPageViewModel
{

}
```

to this

```csharp
public class CustomersPageViewModel : INotifyPropertyChanged
{

}
```

3. In `CustomersPageViewModel.cs`, right click on `INotifyPropertyChanged`
4. Implement the `INotifyPropertyChanged` Interface
   - (Visual Studio PC) In the right-click menu, select Quick Actions and Refactorings -> Implement Interface
5. In `CustomersPageViewModel.cs`, ensure this line of code now appears:

```csharp
public event PropertyChangedEventHandler PropertyChanged;
```

6. In `CustomersPageViewModel.cs`, create a new method called `OnPropertyChanged`
    - Note: We will call `OnPropertyChanged` whenever a property updates

```csharp
public void OnPropertyChanged([CallerMemberName] string name = null) =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
```

### Implementing our data source

Currently we have data being created inside our view XAML file, while this is handy for quickly viewing application designs or relying on static data, we do not want a single set of static customers in our app - we want our customer base to grow! To cater for this we are going to dynamically load data (well pretend to for now).

```csharp
public class CustomersPageViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Customer> _customers;

    public ObservableCollection<Customer> Customers
    {
        get => _customers;
        set
        {
            if (_customers == value)
            {
                return;
            }

            _customers = value;
            OnPropertyChanged();
        }
    }
}
```

Notice that we call `OnPropertyChanged` when the value changes. The .NET MAUI binding infrastructure will subscribe to our **PropertyChanged** event so the UI will be notified of the change.

### Simplifying MVVM with .NET Community Toolkit

Now that you have an understanding of how MVVM works, let's look at a way to simplify development. As applications get more complex, more properties and events will be added. This leads to more boilerplate code being added. The .NET Community Toolkit seeks to simplify MVVM with source generators to automatically handle the code that we used to manually had to write.

We first need to add the `CommunityToolkit.Mvvm` package to the project via NuGet:

* Right click on the `Cupcakes` project
* Select Manage NuGet Packages...
* Type `CommunityToolkit.Mvvm` in the search bar
* Select `CommunityToolkit.Mvvm` result
* Click install

Delete all contents in `CustomersPageViewModel.cs` and replace it with the following:

```csharp
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cupcakes.Customers.ViewModels;

public partial class CustomersPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Customer> _customers;
}
```

Here, we can see that our code has been greatly simplified with an `ObservableObject` base class that implements `INotifyPropertyChanged` and also attributes to expose our properties.

Note that both `_customers` has the `[ObservableProperty]` attribute attached to it. The code that is generated looks nearly identical to what we manually wrote. To see the generated code head to the project and then expand **Dependencies -> net8.0-android -> Analyzers -> CommunityToolkit.Mvvm.SourceGenerators -> CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator** and open `Cupcakes.Customers.ViewModels.CustomersPageViewModel.cs`:

Here is what our `Customers` property looks like:

```csharp
[global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.0.0.0")]
[global::System.Diagnostics.DebuggerNonUserCode]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public ObservableCollection<Customer> Customers
{
    get => _customers;
    set
    {
        if (!global::System.Collections.Generic.EqualityComparer<bool>.Default.Equals(_customers, value))
        {
            OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.Customers);
            _customers = value;
            OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Customers);
        }
    }
}
```

This code may look a bit scary, but since it is auto-generated it adds additional attributes to avoid conflicts. It is also highly optimized with caching as well.

The same library will also help us handle interaction events aka `Commands` in the future.

> Note that we changed this class to a `partial` class so the generated code can be shared in the class.

## Hooking our view model up to our view

One of the key elements in using the MVVM pattern is how to connect the view and view model together. THe way to connect them in our .NET MAUI application is to create a dependency from the view on the view model, we can do this by adding the `CustomersPageViewModel` as a parameter to the `CustomersPage` constructor.

To do so open the `Cupcakes\Customers\Pages\CustomersPage.xaml.cs` file and change the following:

```csharp
namespace Cupcakes.Customers.Pages;

public partial class CustomersPage : ContentPage
{
    public CustomersPage()
    {
        InitializeComponent();
    }
}
```

to include the dependency on `CustomersPageViewModel` as follows:

```csharp
using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers.Pages;

public partial class CustomersPage : ContentPage
{
    public CustomersPage(CustomersPageViewModel customersPageViewModel)
    {
        InitializeComponent();
        BindingContext = customersPageViewModel;
        _customersPageViewModel = customersPageViewModel;
    }

    private readonly CustomersPageViewModel _customersPageViewModel;
}
```

You will notice that we also added a line into the constructor, we will soon be creating bindings in our view to properties on our view model, in order to make this work we need to tell the view where to source the values from, we do this through setting the `BindingContext` property.

## Registering with dependency injection

If we were to try and run our application now it will fail, quite often I like to suggest that you do this as I find exposing yourself to some of the common error scenarios can be a beneficial thing.

Let's try it, running on iOS I see the following error message nestled among a long stack trace/error message (note that the full log entry was 35 lines long and we only need worry about this one line):

```log
2024-07-01 20:38:12.626675+0100 Cupcakes[60910:1872297] 
Unhandled Exception:
System.MissingMethodException: No parameterless constructor defined for type 'Cupcakes.Customers.Pages.CustomersPage'.
```

What this error tells us is that the framework (.NET MAUI) is unable to create an instance of our `CustomersPage` and this is because we have added a parameter to the previously parameterless constructor. Fear not though! We just need to register our page and it's dependencies with the dependency injection layer so then .NET MAUI can create our page for us.

### Registering

I like to encapsulate the registration of related dependencies within a common area so that we don't end up bloating our `MauiProgram` file. In order to do this we will first need to create a new file; add a new `class` file to the `Customers` folder and call it `ServiceCollectionExtensions`. We will now create an extension method to simplify the registration of our implementations.

We can replace the current contents of:

with the following:

```csharp
using Cupcakes.Customers.Pages;
using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers;

public static class ServiceCollectionExtensions
{
    public static void AddCustomers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<CustomersPage>();
        serviceCollection.AddTransient<CustomersPageViewModel>();
    }
}
```

This provides us with the ability to use the `AddCustomers` method in our `MauiProgram` file, so let's do that now:

First we need to add a new using statement at the top of our file:

```csharp
using Cupcakes.Customers;
```

And then we can add the following line inside the `CreateMauiApp` method and before the `return builder.Build();` line:

```csharp
builder.Services.AddCustomers();
```

One part that I really like about how we have organized our files is that they are tied to the feature that they represent, that means if we decide in some apps we don't need a specific feature we could avoid registering it, or if our application grows in size we could relatively easily move re-organize our solution and move each feature out to their own project.

If we run our application again we will now see that it doesn't crash but shows the Customers tab just as it did before.

## Loading our customers

We have some final steps to follow to source our data from our view model.

### Add the loading into our view model

We need to add a method to our view model that will be responsible for loading our data, currently we will just hardcode the data but this could easily be replaced (and will be later on) with database access. Let's open our `CustomersPageViewModel.cs` file and add the following method inside the class:

```csharp
public void OnNavigatedTo()
{
    Customers =
    [
        new Customer { Name = "Cookie Monster", Address = "1 Sesame Street" },
        new Customer { Name = "Sherlock Holmes", Address = "221B, Baker Street, London"}
    ];
}
```

### Call the loading method from our view

Currently our view model doesn't have the ability to receive lifecycle events that applies to the view, such as being navigated to, etc. but we can override the method in our page and pass that across to the view model. To do so we can add the following method into our `CustomersPage.xaml.cs` file:

```csharp
protected override void OnNavigatedTo(NavigatedToEventArgs args)
{
    base.OnNavigatedTo(args);
    _customersPageViewModel.OnNavigatedTo();
}
```

### Bind to our Customers property

The final step in sourcing the data from our view model is now to create the `Binding` to the `Customers` property on the `CustomersPageViewModel`. To do so let's open the `CustomersPage.xaml` file and modify the following lines:

```xaml
<CollectionView Margin="10">
            
    <CollectionView.ItemsSource>
        <x:Array Type="{x:Type customers:Customer}">
            <customers:Customer
                Name="Sherlock Holmes"
                PhoneNumber="0123456789"
                Address="221B Baker Street" />
        </x:Array>
    </CollectionView.ItemsSource>
```

down to this:

```xaml
<CollectionView Margin="10" ItemsSource="{Binding Customers}">
```

We will see that rather than using the hardcode array defined in the XAML we are now binding the `ItemsSource` property of the `CollectionView` to the `Customers` property.

If we run the application now we will see that we have 2 customers in our system.

## Compiled bindings

One final parting message on bindings, yes they are a little magical - by default there is no concrete typing and it requires some level of Reflection at runtime, this means there can be a performance overhead and you can also quite simply bind to the wrong thing. In order to avoid this we can make use of a concept called Compiled Bindings. To make use of this we need to define the `DataType` that is being used.

Let's open the `CustomersPage.xaml` file and do this now:

We can modify the top element:

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:customers="clr-namespace:Cupcakes.Customers"
             x:Class="Cupcakes.Customers.Pages.CustomersPage">
```

and replace it with:

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:customers="clr-namespace:Cupcakes.Customers"
             xmlns:customerViewModels="clr-namespace:Cupcakes.Customers.ViewModels"
             x:Class="Cupcakes.Customers.Pages.CustomersPage"
             x:DataType="customerViewModels:CustomersPageViewModel">
```

The key changes being:

* adding the the namespace usage: `xmlns:customerViewModels="clr-namespace:Cupcakes.Customers.ViewModels"`
* then defining the `x:DataType="customerViewModels:CustomersPageViewModel"`

You should notice now that if were to retype the `ItemsSource` binding on the `CollectionView` you will receive intellisense showing you the possible properties to bind to and if you spell the property name wrong the code will no longer compile.

## More links and resources

It is worth stating that while MVVM is the most common pattern used within .NET MAUI applications, it doesn't mean that you have to use it. Below are some links to alternative options or extensions to the traditional MVVM approach.

* [Reactive](https://www.reactiveui.net)
* [MVU](https://github.com/adospace/reactorui-maui)
* [C# Markup](https://github.com/CommunityToolkit/Maui.Markup)
* [.NET Community Toolkit](https://github.com/CommunityToolkit/dotnet)

Let's continue and learn about data access in .NET MAUI in [Part 3](../part-3-data-layer/readme.md)
