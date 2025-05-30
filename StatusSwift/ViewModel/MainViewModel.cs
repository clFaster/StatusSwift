using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using Microsoft.Extensions.Logging;
using SharpHook;
using StatusSwift.Services;
using Timer = System.Timers.Timer;

namespace StatusSwift.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly ILogger<MainViewModel> _logger;
    private readonly IStatusSwiftService _statusSwiftService;

    [ObservableProperty] private string _buttonText = "Enable StatusSwift";

    private Timer? _elapsedTimer;

    [ObservableProperty] private string _elapsedTimeText = "00:00:00";

    [ObservableProperty] private bool _isActive;

    private DateTime _startTime;

    [ObservableProperty] private string _trayTooltipText = "StatusSwift - Inactive";

    [ObservableProperty] private string _windowText = "Show Window";

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
        _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<MainViewModel>();
        _statusSwiftService = new StatusSwiftService(_logger, new EventSimulator());
    }


    [RelayCommand]
    public void ToggleStatusSwiftActive()
    {
        if (IsActive)
            DisableStatusSwift();
        else
            EnableStatusSwift();
    }

    [RelayCommand]
    public void EnableStatusSwift()
    {
        IsActive = true;
        _statusSwiftService.StartStatusSwift();

        // Start tracking elapsed time
        _startTime = DateTime.Now;
        _elapsedTimer = new Timer(1000);
        _elapsedTimer.Elapsed += OnTimerElapsed!;
        _elapsedTimer.Start();
        ElapsedTimeText = "00:00:00";
        TrayTooltipText = "StatusSwift - Active\nRunning for: 00:00:00";
        ButtonText = "Disable StatusSwift";
    }

    [RelayCommand]
    public void DisableStatusSwift()
    {
        IsActive = false;
        _statusSwiftService.StopStatusSwift();

        // Stop tracking elapsed time
        _elapsedTimer?.Stop();
        _elapsedTimer?.Dispose();
        _elapsedTimer = null;
        ElapsedTimeText = "00:00:00";
        TrayTooltipText = "StatusSwift - Inactive";
        ButtonText = "Enable StatusSwift";

        _logger.LogDebug("StatusSwift - Disabled");
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        var elapsed = DateTime.Now - _startTime;
        var formattedTime = elapsed.ToString(@"hh\:mm\:ss");

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
        if (window == null) return;

        window.Show();
        window.Activate();

        WindowText = "Show Window";
    }

    [RelayCommand]
    public static void ExitApplication()
    {
        Application.Current?.Quit();
    }
}