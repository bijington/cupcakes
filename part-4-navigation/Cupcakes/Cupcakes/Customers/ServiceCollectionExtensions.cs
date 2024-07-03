using CommunityToolkit.Maui;
using Cupcakes.Customers.Pages;
using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers;

public static class ServiceCollectionExtensions
{
    public static void AddCustomers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<CustomersPage>();
        serviceCollection.AddTransient<CustomersPageViewModel>();
        
        serviceCollection.AddTransientWithShellRoute<AddCustomerPage, AddCustomerPageViewModel>(RouteNames.AddCustomer);
        serviceCollection.AddTransientWithShellRoute<CustomerDetailsPage, CustomerDetailsPageViewModel>(RouteNames.CustomerDetails);
        
        serviceCollection.AddSingleton(FileSystem.Current);
        serviceCollection.AddSingleton<CustomerRepository>();
    }
}