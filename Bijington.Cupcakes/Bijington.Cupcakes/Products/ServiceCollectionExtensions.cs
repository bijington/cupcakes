using Bijington.Cupcakes.Products.Pages;
using Bijington.Cupcakes.Products.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace Bijington.Cupcakes.Products;

public static class ServiceCollectionExtensions
{
    public static void AddProducts(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ProductsPage>();
        serviceCollection.AddTransient<ProductsPageViewModel>();

        serviceCollection.AddTransientWithShellRoute<AddProductPage, AddProductPageViewModel>(RouteNames.AddProduct);

        serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
    }
}