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
        InitializeComponent();

        logger.LogInformation("App is starting...");
        
        // Initialize scheduler
        scheduler = new StdSchedulerFactory().GetScheduler().Result;
        scheduler.Start();

        // Define and schedule a job
        var job = JobBuilder.Create<YourJobClass>()
            .WithIdentity("yourJobName", "group1")
            .Build();

        job.JobDataMap["logger"] = logger;

        var trigger = TriggerBuilder.Create()
            .WithIdentity("yourTriggerName", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(1)  // Set your desired interval
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

public class YourJobClass : IJob
{
    private ILogger<YourJobClass> _logger;

    public async Task Execute(IJobExecutionContext context)
    {
        // Your job logic goes here
        //_logger = context.JobDetail.JobDataMap.Get("logger");
        //_logger.LogInformation("Job is running...");
    }
}