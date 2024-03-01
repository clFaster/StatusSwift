using Microsoft.Extensions.Logging;
using StatusSwift.ViewModel;

namespace StatusSwift;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        ConfigureServices(builder.Services);
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        #if DEBUG
		builder.Logging.AddDebug();
        #endif

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainPage>();
        services.AddSingleton<MainViewModel>();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
        });
    }
}
