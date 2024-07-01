using Bijington.Cupcakes.Customers;
using Bijington.Cupcakes.Orders;
using Bijington.Cupcakes.Products;
using Bijington.Cupcakes.Settings;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Bijington.Cupcakes;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddCustomers();
        builder.Services.AddOrders();
        builder.Services.AddProducts();
        builder.Services.AddSettings();

        builder.Services.AddSingleton(FileSystem.Current);
        builder.Services.AddSingleton(Geocoding.Default);
        builder.Services.AddSingleton(Map.Default);
        builder.Services.AddSingleton(MediaPicker.Default);
        builder.Services.AddSingleton(PhoneDialer.Default);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}