using Cupcakes.Customers.Pages;
using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers;

public static class ServiceCollectionExtensions
{
    public static void AddCustomers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<CustomersPage>();
        serviceCollection.AddTransient<CustomersPageViewModel>();
        
        serviceCollection.AddTransient<AddCustomerPage>();
        serviceCollection.AddTransient<AddCustomerPageViewModel>();
        Routing.RegisterRoute(RouteNames.AddCustomer, typeof(AddCustomerPage));
        
        serviceCollection.AddSingleton(FileSystem.Current);
        serviceCollection.AddSingleton<CustomerRepository>();
    }
}