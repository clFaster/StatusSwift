using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using Microsoft.Extensions.Logging;
using StatusSwift.Services;
using System.Timers;

namespace StatusSwift.ViewModel;

public partial class MainViewModel : ObservableObject
{   
    private static bool _statusSwiftActive;
    private System.Timers.Timer? _elapsedTimer;
    private DateTime _startTime;
    private readonly ILogger<MainViewModel>? _logger;
    private readonly IStatusSwiftService? _statusSwiftService;

    [ObservableProperty]
    private string _buttonText = "Enable StatusSwift";

    [ObservableProperty]
    private string _windowText = "Show Window";

    [ObservableProperty]
    private string _elapsedTimeText = "00:00:00";

    [ObservableProperty]
    private bool _isActive = false;
    
    [ObservableProperty]
    private string _trayTooltipText = "StatusSwift - Inactive";

    // Main constructor for DI
    public MainViewModel(ILogger<MainViewModel> logger, IStatusSwiftService statusSwiftService)
    {
        _logger = logger;
        _statusSwiftService = statusSwiftService;
    }

    // Design-time constructor
    public MainViewModel()
    {
        // Constructor needed for design-time
    }

    [RelayCommand]
    public void ToggleStatusSwiftActive()
    {
        _statusSwiftActive = !_statusSwiftActive;
        IsActive = _statusSwiftActive;
        ButtonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";
        _logger?.LogInformation("State switched to: {ButtonText}", ButtonText);
        _statusSwiftService?.ToggleTimer(_statusSwiftActive);

        if (_statusSwiftActive)
        {
            // Start tracking elapsed time
            _startTime = DateTime.Now;
            _elapsedTimer = new System.Timers.Timer(1000);
            _elapsedTimer.Elapsed += OnTimerElapsed!;
            _elapsedTimer.Start();
            ElapsedTimeText = "00:00:00";
            TrayTooltipText = "StatusSwift - Active\nRunning for: 00:00:00";
        }
        else
        {
            // Stop tracking elapsed time
            _elapsedTimer?.Stop();
            _elapsedTimer?.Dispose();
            _elapsedTimer = null;
            ElapsedTimeText = "00:00:00";
            TrayTooltipText = "StatusSwift - Inactive";
        }
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        TimeSpan elapsed = DateTime.Now - _startTime;
        string formattedTime = elapsed.ToString(@"hh\:mm\:ss");

        // Update on UI thread
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            ElapsedTimeText = formattedTime;
            TrayTooltipText = $"StatusSwift - Active\nRunning for: {formattedTime}";
        });
    }

    [RelayCommand]
    public void ShowWindow()
    {
        var window = Application.Current?.MainPage?.Window;
        if (window == null)
        {
            return;
        }
        
        window.Show();
        window.Activate();
        
        WindowText = "Show Window";
    }

    [RelayCommand]
    public void ExitApplication()
    {
        Application.Current?.Quit();
    }
}