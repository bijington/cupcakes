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