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
            Width = 550,
            Height = 500,
            MinimumWidth = 450,
            MinimumHeight = 500
        };
}