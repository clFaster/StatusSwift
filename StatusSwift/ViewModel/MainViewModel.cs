using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using Microsoft.Extensions.Logging;
using StatusSwift.Services;

namespace StatusSwift.ViewModel;

public partial class MainViewModel(ILogger<MainViewModel> logger, IStatusSwiftService statusSwiftService) : ObservableObject
{   
    private static bool _statusSwiftActive;

    [ObservableProperty]
    private string _buttonText = "Enable StatusSwift";

    [ObservableProperty]
    private string _windowText = "Show Window";

    [RelayCommand]
    public void ToggleStatusSwiftActive()
    {
        _statusSwiftActive = !_statusSwiftActive;
        ButtonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";
        logger.LogInformation("State switched to: {ButtonText}", ButtonText);
        statusSwiftService.ToggleTimer(_statusSwiftActive);
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