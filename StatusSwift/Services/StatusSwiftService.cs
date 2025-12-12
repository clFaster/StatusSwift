using Microsoft.Extensions.Logging;
using SharpHook;
using StatusSwift.BO;

namespace StatusSwift.Services;

public sealed class StatusSwiftService(
    ILogger<StatusSwiftService> logger,
    IEventSimulator simulator,
    TimeProvider timeProvider) : IStatusSwiftService
{
    private CancellationTokenSource? _cancellationTokenSource;

    /// <inheritdoc />
    public void StartStatusSwift()
    {
        if (_cancellationTokenSource is not null)
        {
            logger.LogDebug("Start Status Swift ignored (already running)");
            return;
        }

        logger.LogInformation("Start Status Swift");
        _cancellationTokenSource = new CancellationTokenSource();
        _ = RunAsync(_cancellationTokenSource.Token);
    }

    /// <inheritdoc />
    public void StopStatusSwift()
    {
        logger.LogInformation("Stop Status Swift");

        var cts = Interlocked.Exchange(ref _cancellationTokenSource, null);
        if (cts is null)
        {
            return;
        }

        cts.Cancel();
        cts.Dispose();
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var interval = GetRandomSeconds();
                logger.LogInformation("Move mouse, and schedule again in {Interval} seconds", interval);

                // Simulate Mouse Move
                const short x = 10;
                const short y = 10;
                simulator.SimulateMouseMovementRelative(x, y);
                simulator.SimulateMouseMovementRelative(-x, -y);

                await Task.Delay(TimeSpan.FromSeconds(interval), timeProvider, cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Expected during StopStatusSwift
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "StatusSwift background loop failed");
            StopStatusSwift();
        }
    }

    private static int GetRandomSeconds()
    {
        var min = Preferences.Default.Get(PreferenceKeys.Min_time, 30);
        var max = Preferences.Default.Get(PreferenceKeys.Max_time, 181);
        return Random.Shared.Next(min, max);
    }
}