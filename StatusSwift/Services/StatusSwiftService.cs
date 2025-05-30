using Microsoft.Extensions.Logging;
using SharpHook;
using StatusSwift.BO;
using StatusSwift.ViewModel;

namespace StatusSwift.Services;

public class StatusSwiftService(ILogger<MainViewModel> logger, IEventSimulator simulator) : IStatusSwiftService
{
    private CancellationTokenSource? _cancellationTokenSource;
    private Timer? _timer;

    /// <inheritdoc />
    public void StartStatusSwift()
    {
        logger.LogInformation("Start Status Swift");
        _cancellationTokenSource = new CancellationTokenSource();
        var interval = GetRandomSeconds();
        _timer = new Timer(DoWork, _cancellationTokenSource.Token, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
    }
    
    /// <inheritdoc />
    public void StopStatusSwift()
    {
        logger.LogInformation("Stop Status Swift");
        _timer?.Dispose();
        _cancellationTokenSource?.Dispose();
        _timer = null;
    }

    private void DoWork(object? state)
    {
        if (((CancellationToken)state!).IsCancellationRequested) return;

        var interval = GetRandomSeconds();
        logger.LogInformation("Move mouse, and schedule again in {Interval} seconds", interval);

        // Simulate Mouse Move
        const short x = 10;
        const short y = 10;
        simulator.SimulateMouseMovementRelative(x, y);
        simulator.SimulateMouseMovementRelative(-x, -y);

        _timer?.Change(TimeSpan.FromSeconds(interval), TimeSpan.FromSeconds(interval));
    }

    private static int GetRandomSeconds()
    {
        var random = new Random();
        var interval = random.Next(Preferences.Default.Get(PreferenceKeys.Min_time, 30),
            Preferences.Default.Get(PreferenceKeys.Max_time, 181));
        return interval;
    }
}