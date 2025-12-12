using H.NotifyIcon;
using Microsoft.Extensions.Logging;
using SharpHook;
using StatusSwift.BO;
using StatusSwift.Converters;
using StatusSwift.Services;
using StatusSwift.ViewModel;

namespace StatusSwift;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseNotifyIcon()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        ConfigureServices(builder.Services);

#if DEBUG
        builder.Logging.AddDebug();
        Preferences.Default.Set(PreferenceKeys.Min_time, 2);
        Preferences.Default.Set(PreferenceKeys.Max_time, 5);
#else
        Preferences.Default.Set(PreferenceKeys.Min_time, 30);
        Preferences.Default.Set(PreferenceKeys.Max_time, 181);

#endif

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainPage>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton(TimeProvider.System);

        services.AddSingleton<IStatusSwiftService, StatusSwiftService>();
        services.AddTransient<IEventSimulator, EventSimulator>();

        services.AddSingleton<BoolToTextConverter>();
    }
}