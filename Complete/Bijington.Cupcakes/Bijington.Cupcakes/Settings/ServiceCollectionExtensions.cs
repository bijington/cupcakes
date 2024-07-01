using Bijington.Cupcakes.Settings.Pages;

namespace Bijington.Cupcakes.Settings;

public static class ServiceCollectionExtensions
{
    public static void AddSettings(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<SettingsPage>();

        serviceCollection.AddSingleton<ISettingsRepository, SettingsRepository>();
    }
}