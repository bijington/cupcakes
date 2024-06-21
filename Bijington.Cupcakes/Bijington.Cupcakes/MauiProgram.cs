using Bijington.Cupcakes.Pages;
using Bijington.Cupcakes.ViewModels;
using Microsoft.Extensions.Logging;

namespace Bijington.Cupcakes;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductsPageViewModel>();
        
        builder.Services.AddTransient<AddProductPage>();
        builder.Services.AddTransient<AddProductViewModel>();
        Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
        
        builder.Services.AddTransient<OrdersPage>();
        builder.Services.AddTransient<OrdersPageViewModel>();

        builder.Services.AddSingleton(MediaPicker.Default);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}