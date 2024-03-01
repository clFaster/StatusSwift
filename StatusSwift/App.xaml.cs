using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

namespace StatusSwift;

public partial class App() : Application
{
    private readonly ILogger<App> _logger;
    IScheduler scheduler;
    
    public App(ILogger<App> logger) : this()
    {
        _logger = logger;
        InitializeComponent();

        _logger.LogInformation("App is starting...");
        
        // Initialize scheduler
        scheduler = new StdSchedulerFactory().GetScheduler().Result;
        scheduler.Start();

        // Define and schedule a job
        var job = JobBuilder.Create<YourJobClass>()
            .WithIdentity("yourJobName", "group1")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("yourTriggerName", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)  // Set your desired interval
                .RepeatForever())
            .Build();

        scheduler.ScheduleJob(job, trigger);
        
        
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

public class YourJobClass(ILogger<App> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        // Your job logic goes here
        logger.LogInformation("Job is running...");
    }
}