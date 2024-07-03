# Part 1 - Displaying data

In this part we will be adding the first page into our application and making it show some data.

## Setup

We have a few steps to follow but one key detail to highlight is that we will be creating a top-level folder for each part of our application (e.g. Customers, Products, etc.).

Let's make the following changes:

* Add a new folder to the root of the project and call it `Customers`.
* Inside the new `Customers` folder create a new folder and call it `Pages`.
* Add a new `ContentPage` inside the `Pages` folder and call it `CustomersPage`.
* Delete the `MainPage.xaml` file that was created by default.

We now have a solution that won't compile but don't worry the next steps will talk through some concepts and show how we can fix things.

## Add our tab icons

We are going to jump ahead of ourselves a little and add all of the images from the [images folder](../images/) to save some effort in the following parts.

Grab the files from the above folder and place them under the `Cupcakes/Resources/Images`. You can do this via the file system or your IDE, the result will be the same.

## Introducing Tabs

Our application is built using [Shell](https://learn.microsoft.com/dotnet/maui/fundamentals/shell/create) and for the next part we will be adding [Tabs](https://learn.microsoft.com/dotnet/maui/fundamentals/shell/tabs)

Delete the following entry inside `AppShell.xaml`

```xaml
<ShellContent
    Title="Home"
    ContentTemplate="{DataTemplate local:MainPage}"
    Route="MainPage" />
```

and replace it with:

```xaml
<TabBar>
    <ShellContent
        Title="Customers"
        Icon="customers.png"
        ContentTemplate="{DataTemplate customersPages:CustomersPage}" />
</TabBar>
```

> One key detail to note is that we copied an image file called `customers.svg` but we are referring to the image file as `customers.png` this is due to the compilation details mentioned in [Part 0](../Part%200%20-%20Overview/readme.md#images). The SVG will be used during compilation to generate appropriately sized images that can be rendered in our applications.

We are now making use of a new namespace within our project. Just like when using C# we need to include namespaces in our XAML files. We want to add the following to our `Shell` element.

```xaml
xmlns:customersPages="clr-namespace:Cupcakes.Customers.Pages"
```

The full `Shell` element should look like the following:

```xaml
<Shell
    x:Class="Cupcakes.AppShell"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:customersPages="clr-namespace:Cupcakes.Customers.Pages"
    xmlns:local="clr-namespace:Cupcakes"
    Shell.FlyoutBehavior="Disabled"
    Title="Cupcakes">
```

If you run the application now you will see that we have a blank page showing and the title of Customers. You will also notice that there is no tab bar showing at the bottom of the application, .NET MAUI won't show a tab bar if there is only 1 tab, this is most likely to save on screen real estate, this can be a precious thing when dealing with mobile based devices.

To give the illusion of more content to our application and something to fill in we can add more tabs into our `TabBar`, after the Customers `ShellContent` entry we can add the following:

```xaml
<ShellContent
    Title="Products"
    Icon="products.png" />

<ShellContent
    Title="Orders"
    Icon="orders.png" />
        
<ShellContent
    Title="Analytics"
    Icon="analytics.png" />
        
<ShellContent
    Title="Settings"
    Icon="settings.png" />
```

## Creating our models

We first need to add a class that will represent our `Customer` data. Let's add a new class file to the `Customers` folder and call it `Customer`.

We can replace the class definition with the following:

```csharp
public class Customer
{
    public string Address { get; init; } = string.Empty;
    
    public string Name { get; init; } = string.Empty;
    
    public string PhoneNumber { get; init; } = string.Empty;
    
    public int Id { get; set; }
}
```

## Displaying our data

```xaml
<CollectionView Margin="10">
            
    <CollectionView.ItemsSource>
        <x:Array Type="{x:Type customers:Customer}">
            <customers:Customer
                Name="Sherlock Holmes"
                PhoneNumber="0123456789"
                Address="221B Baker Street" />
            <customers:Customer
                Name="Cookie Monster"
                PhoneNumber="4"
                Address="1 Sesame Street" />
        </x:Array>
    </CollectionView.ItemsSource>
            
    <CollectionView.ItemTemplate>
        <DataTemplate x:DataType="customers:Customer">
            <Border StrokeShape="RoundRectangle 25">
                <VerticalStackLayout Padding="30,0,0,0">
                    <Label Text="{Binding Path=Name}" FontSize="30"/>
                    <Label Text="{Binding Path=Address}" />
                    <Label Text="{Binding Path=PhoneNumber}" />
                </VerticalStackLayout>
            </Border>
        </DataTemplate>
    </CollectionView.ItemTemplate>

 </CollectionView>
```

### Run the App

Ensure that you have your machine setup to deploy and debug to the different platforms:

* [Android Emulator Setup](https://docs.microsoft.com/dotnet/maui/android/emulator/device-manager)
* [Windows setup for development](https://docs.microsoft.com/dotnet/maui/windows/setup)

1. In Visual Studio, set the Android or Windows app as the startup project by selecting the drop down in the debug menu and changing the `Framework`
2. In Visual Studio, click the "Debug" button or Tools -> Start Debugging
    * If you are having any trouble, see the Setup guides for your runtime platform

Running the app will result in a list of 2 customers.

Let's continue and learn about using the MVVM pattern with data binding in [Part 2](../part-2-mvvm/readme.md)
