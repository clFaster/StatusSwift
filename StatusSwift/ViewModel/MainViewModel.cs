using System.Diagnostics;
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

    private readonly Stopwatch _stopwatch = new();

    private Timer? _elapsedTimer;

    public string ButtonText
    {
        get;
        private set => SetProperty(ref field, value);
    } = "Enable StatusSwift";

    public string ElapsedTimeText
    {
        get;
        private set => SetProperty(ref field, value);
    } = "00:00:00";

    public bool IsActive
    {
        get;
        private set => SetProperty(ref field, value);
    }

    public string TrayTooltipText
    {
        get;
        private set => SetProperty(ref field, value);
    } = "StatusSwift - Inactive";

    public string WindowText
    {
        get;
        private set => SetProperty(ref field, value);
    } = "Show Window";

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
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<MainViewModel>();
        _statusSwiftService = new StatusSwiftService(
            loggerFactory.CreateLogger<StatusSwiftService>(),
            new EventSimulator(),
            TimeProvider.System);
    }


    [RelayCommand]
    private void ToggleStatusSwiftActive()
    {
        if (IsActive)
            DisableStatusSwift();
        else
            EnableStatusSwift();
    }

    [RelayCommand]
    private void EnableStatusSwift()
    {
        IsActive = true;
        _statusSwiftService.StartStatusSwift();

        // Start tracking elapsed time
        _stopwatch.Reset();
        _stopwatch.Start();
        _elapsedTimer = new Timer(1000);
        _elapsedTimer.Elapsed += OnTimerElapsed;
        _elapsedTimer.Start();
        ElapsedTimeText = "00:00:00";
        TrayTooltipText = "StatusSwift - Active\nRunning for: 00:00:00";
        ButtonText = "Disable StatusSwift";
    }

    [RelayCommand]
    private void DisableStatusSwift()
    {
        IsActive = false;
        _statusSwiftService.StopStatusSwift();

        // Stop tracking elapsed time
        _elapsedTimer?.Stop();
        _elapsedTimer?.Dispose();
        _elapsedTimer = null;
        _stopwatch.Stop();
        ElapsedTimeText = "00:00:00";
        TrayTooltipText = "StatusSwift - Inactive";
        ButtonText = "Enable StatusSwift";

        _logger.LogDebug("StatusSwift - Disabled");
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        var elapsed = _stopwatch.Elapsed;
        var formattedTime = elapsed.ToString(@"hh\:mm\:ss");

        // Update on UI thread
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            ElapsedTimeText = formattedTime;
            TrayTooltipText = $"StatusSwift - Active\nRunning for: {formattedTime}";
        });
    }

    [RelayCommand]
    private void ShowWindow()
    {
        var window = Application.Current?.Windows.FirstOrDefault();
        if (window == null) return;

        window.Show();
        window.Activate();

        WindowText = "Show Window";
    }

    [RelayCommand]
    private void ExitApplication()
    {
        DisableStatusSwift();
        _logger.LogInformation("Exiting application...");
        Application.Current?.Quit();
    }
}