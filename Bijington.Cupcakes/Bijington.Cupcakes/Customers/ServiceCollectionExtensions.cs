using Bijington.Cupcakes.Customers.Pages;
using Bijington.Cupcakes.Customers.ViewModels;
using CommunityToolkit.Maui;

namespace Bijington.Cupcakes.Customers;

public static class ServiceCollectionExtensions
{
    public static void AddCustomers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<CustomersPage>();
        serviceCollection.AddTransient<CustomersPageViewModel>();

        serviceCollection.AddTransientWithShellRoute<AddCustomerPage, AddCustomerPageViewModel>(RouteNames.AddCustomer);

        serviceCollection.AddSingleton<ICustomerRepository, CustomerRepository>();
    }
}