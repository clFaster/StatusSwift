using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpHook;

namespace StatusSwift.ViewModel;

public partial class MainViewModel: ObservableObject
{
    private Timer? _timer;
    private CancellationTokenSource? _cancellationTokenSource;
    
    private static bool _statusSwiftActive;

    [ObservableProperty]
    private string _buttonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";

    [RelayCommand]
    private void ToggleStatusSwiftActive()
    {
        _statusSwiftActive = !_statusSwiftActive;
        ButtonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";
        ToggleTimer(_statusSwiftActive);
    }
    private void DoWork(object? state)
    {
        if (((CancellationToken)state!).IsCancellationRequested)
        {
            return;
        }
        var random = new Random();
        var interval = random.Next(30, 181); // Generate a random integer between 30 and 180
        
        var simulator = new EventSimulator();
        simulator.SimulateMouseMovementRelative(1, 0);
        
        _timer?.Change(TimeSpan.FromSeconds(interval), TimeSpan.FromSeconds(interval));
    }
    
    
    private void ToggleTimer(bool statusSwiftActive)
    {
        if (statusSwiftActive)
        {
            // Create a new CancellationTokenSource
            _cancellationTokenSource = new CancellationTokenSource();

            // Create a Timer that triggers the job every x seconds
            var random = new Random();
            // Generate a random integer between 30 and 180
            var interval = random.Next(30, 181);
            
            _timer = new Timer(DoWork, _cancellationTokenSource.Token, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
        }
        else
        {
            // Stop the timer and dispose of the CancellationTokenSource
            _timer?.Dispose();
            _cancellationTokenSource?.Dispose();
            _timer = null;
        }
    }
}