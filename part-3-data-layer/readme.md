# Part 3 - Data layer

In this part we are going to add a data layer for this we will make use of the repository pattern and hide our data access behind it. We will also make use of an SQLite database.

## Include the `sqlite-net-pcl` NuGet package

We first need to add the `sqlite-net-pcl` package to the project via NuGet:

* Right click on the `Cupcakes` project
* Select Manage NuGet Packages...
* Type `sqlite-net-pcl` in the search bar
* Select `sqlite-net-pcl` result
* Click install

This package will now be installed and ready for us to use.

## Adding our CustomerRepository

The first step will be to create our repository class, let's do this by adding a new `class` file to the `Customers` folder and call it `CustomerRepository`.

> Note that quite often you might find that developer will create an interface for something like this, having an interface can sometime make it easier to setup unit testing but sometimes it can just create additional overhead. The decision whether to or not is not always clearly defined but I would urge that you really consider whether you need the interface, if not just stick with the class.

### Update our `Customer` class

We need to make some changes in the `Customer` class, we want to add 2 attributes onto the `Id` property. We can change the following:

```csharp
namespace Cupcakes.Customers;

public class Customer
{
    public string Address { get; init; } = string.Empty;
    
    public string Name { get; init; } = string.Empty;
    
    public string PhoneNumber { get; init; } = string.Empty;

    public int Id { get; set; }
}
```

to this:

```csharp
using SQLite;

namespace Cupcakes.Customers;

public class Customer
{
    public string Address { get; init; } = string.Empty;
    
    public string Name { get; init; } = string.Empty;
    
    public string PhoneNumber { get; init; } = string.Empty;
    
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
```

The 2 key changes are:

* adding the `using SQLite;` statement.
* adding 2 attributes to the `Id` property to mark it as the primary key and enable the database library to automatically generate new ids for us.

### Implementing our `CustomerRepository`

Now that we have the NuGet package installed and we have marked out `Customer` class ready to use we can now implement our data access.

We will do this in stages just to draw emphasis to each set of change:

#### Add the using statement

First we need to add the using statement in order to refer to the library features.

```csharp
using SQLite;
```

#### Creating a connection to the database

Sqlite database are contained within a single file, when we create our connection we need to pass in the path to that file. If the database file doesn't exist then the library will create one for us.

We will store the database under the applications data directory, this is different for each platform that .NET MAUI supports and thankfully .NET MAUI provides us with the `IFileSystem` interface that abstracts away the platform differences for us.

This means that we can write the following:

```csharp
private readonly SQLiteAsyncConnection _connection;

public CustomerRepository(IFileSystem fileSystem)
{
    var dbPath = Path.Combine(fileSystem.AppDataDirectory, "cupcakes_sqlite.db");

    _connection = new SQLiteAsyncConnection(dbPath);
    _connection.CreateTableAsync<Customer>();
}
```

This will result in a file called `cupcakes_sqlite.db` being created in the applications data directory.

The `CreateTableAsync` will create the database table inside our application based on the class (our `Customer` class) that we pass it.

#### Add a `GetCustomers` method

Our current `CustomersPageViewModel` shows a list of customers, in order to populate this with dynamic data we will need to load this from the database.

Let's add a `GetCustomers` method and load the data from the database.

```csharp
public async Task<IReadOnlyCollection<Customer>> GetCustomers()
{
    return await _connection.Table<Customer>().ToListAsync();
}
```

#### Add a `Save` method

Now that we have a database of customers we will need a way to insert some data into it, otherwise it will be rather empty.

```csharp
public Task Save(Customer customer)
{
    return _connection.InsertAsync(customer);
}
```

We won't be using the `Save` method in this part but it sets us up nicely for Part 4.

To confirm the final `CustomerRepository` file should look like:

```csharp
using System.Collections.Immutable;
using SQLite;

namespace Cupcakes.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly SQLiteAsyncConnection _connection;
    
    public CustomerRepository(IFileSystem fileSystem)
    {
        var dbPath = Path.Combine(fileSystem.AppDataDirectory, "cupcakes_sqlite.db");
    
        _connection = new SQLiteAsyncConnection(dbPath);
        _connection.CreateTableAsync<Customer>();
    }
    
    public async Task<IReadOnlyCollection<Customer>> GetCustomers()
    {
        return await _connection.Table<Customer>().ToListAsync();
    }
    
    public Task Save(Customer customer)
    {
        return _connection.InsertAsync(customer);
    }
}
```

## Using the `CustomerRepository`

We now need to open our `CustomersPageViewModel` file and make the following changes:

### Add a dependency to the `CustomerRepository`

We can add a constructor into our class that brings in the `CustomerRepository` as a parameter and assigns it to a backing field.

```csharp
public CustomersPageViewModel(CustomerRepository customerRepository)
{
    _customerRepository = customerRepository;
}

private readonly CustomerRepository _customerRepository;
```

### Load the customers from the repository

We can then change our `OnNavigatedTo` method implementation from:

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

to the following:

```csharp
public async void OnNavigatedTo()
{
    Customers = new ObservableCollection<Customer>(await _customerRepository.GetCustomers());
}
```

The finished view model code should look as follows:

```csharp
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cupcakes.Customers.ViewModels;

public partial class CustomersPageViewModel : ObservableObject
{
    public CustomersPageViewModel(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    private readonly CustomerRepository _customerRepository;
    
    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];
    
    public async void OnNavigatedTo()
    {
        Customers = new ObservableCollection<Customer>(await _customerRepository.GetCustomers());
    }
}
```

## Update our `AddCustomers` extension method

We have registered 2 new dependencies with the changes that we have introduced:

* the `CustomerRepository` now depends on the `IFileSystem` implementation provided by .NET MAUI
* the `CustomersPageViewModel` now depends on the `CustomerRepository` that we have created

If we were to run our application now we will see an exception thrown, let's do this as we have done before to gain some understanding of the errors that we might encounter during development:

> System.InvalidOperationException: Unable to resolve service for type 'Cupcakes.Customers.CustomerRepository' while attempting to activate 'Cupcakes.Customers.ViewModels.CustomersPageViewModel'.

As the error message states the dependency injection layer was unable to provide an instance of the `CustomerRepository` when creating an instance of the `CustomersPageViewModel` class.

To solve this issue we need to register the 2 new dependencies that we mentioned in the list above.

We can change the current method from:

```csharp
public static void AddCustomers(this IServiceCollection serviceCollection)
{
    serviceCollection.AddTransient<CustomersPage>();
    serviceCollection.AddTransient<CustomersPageViewModel>();
}
```

to the following:

```csharp
public static void AddCustomers(this IServiceCollection serviceCollection)
{
    serviceCollection.AddTransient<CustomersPage>();
    serviceCollection.AddTransient<CustomersPageViewModel>();
    
    serviceCollection.AddSingleton(FileSystem.Current);
    serviceCollection.AddSingleton<CustomerRepository>();
}
```

One key detail is the method `AddSingleton` this means that for every class/instance that depends on the `CustomerRepository` the **same** instance will be always be provided.

## Finishing up

If we run our application we will be back to seeing an empty page, that's because there isn't any data in our database. Let's proceed swiftly onto [Part 4](../part-4-navigation/readme.md) which will allow our users to add data to the database.
