using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

namespace StatusSwift;

public partial class App() : Application
{
    public App(ILogger<App> logger) : this()
    {
        InitializeComponent();

        logger.LogInformation("App is starting...");  
        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnResume()
    {
        base.OnResume();
    }

    protected override void OnSleep()
    {
        base.OnSleep();
    }
}