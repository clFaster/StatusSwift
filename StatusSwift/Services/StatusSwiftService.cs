﻿using Microsoft.Extensions.Logging;
using SharpHook;
using StatusSwift.BO;
using StatusSwift.ViewModel;
using System.Threading;

namespace StatusSwift.Services;

public class StatusSwiftService(ILogger<MainViewModel> logger, IEventSimulator simulator): IStatusSwiftService
{
    private Timer? _timer;
    private CancellationTokenSource? _cancellationTokenSource;

    public void ToggleTimer(bool statusSwiftActive)
    {
        if (statusSwiftActive)
        {
            logger.LogInformation($"Start Timer");
            _cancellationTokenSource = new CancellationTokenSource();

            var interval = GetRandomSeconds();

            _timer = new Timer(DoWork, _cancellationTokenSource.Token, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
        }
        else
        {
            logger.LogInformation($"Stop Timer");
            _timer?.Dispose();
            _cancellationTokenSource?.Dispose();
            _timer = null;
        }
    }

    private void DoWork(object? state)
    {
        if (((CancellationToken)state!).IsCancellationRequested)
        {
            return;
        }


        var interval = GetRandomSeconds();

        logger.LogInformation("Move mouse, and schedule again in {interval} seconds.", interval);

        simulator.SimulateMouseMovementRelative(10, 0);

        _timer?.Change(TimeSpan.FromSeconds(interval), TimeSpan.FromSeconds(interval));
    }

    private static int GetRandomSeconds()
    {
        var random = new Random();
        var interval = random.Next(Preferences.Default.Get(PreferenceKeys.Min_time, 30), Preferences.Default.Get(PreferenceKeys.Max_time, 181));
        return interval;
    }
}