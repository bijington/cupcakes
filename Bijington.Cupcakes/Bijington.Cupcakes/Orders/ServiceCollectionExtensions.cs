using Bijington.Cupcakes.Orders.Pages;
using Bijington.Cupcakes.Orders.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace Bijington.Cupcakes.Orders;

public static class ServiceCollectionExtensions
{
    public static void AddOrders(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<OrdersPage>();
        serviceCollection.AddTransient<OrdersPageViewModel>();

        serviceCollection.AddTransientWithShellRoute<AddOrderPage, AddOrderPageViewModel>(RouteNames.AddOrder);

        serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
    }
}