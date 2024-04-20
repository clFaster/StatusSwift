using Microsoft.Extensions.Logging;

namespace StatusSwift;

public partial class App() : Application
{
    public App(ILogger<App> logger) : this()
    {
        InitializeComponent();

        logger.LogInformation("App is starting...");  
    }

    protected override Window CreateWindow(IActivationState? activationState) =>
        new(new AppShell())
        {
            Width = 500,
            Height = 300
        };
}