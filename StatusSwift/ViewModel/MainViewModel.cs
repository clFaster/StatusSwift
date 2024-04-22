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
    private string _buttonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";

    [RelayCommand]
    private void ToggleStatusSwiftActive()
    {
        _statusSwiftActive = !_statusSwiftActive;
        ButtonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";
        logger.LogInformation("State switched to: {ButtonText}", ButtonText);
        statusSwiftService.ToggleTimer(_statusSwiftActive);
    }
    
    
    private bool IsWindowVisible { get; set; } = true;
    
    [RelayCommand]
    public void ShowHideWindow()
    {
        var window = Application.Current?.MainPage?.Window;
        if (window == null)
        {
            return;
        }

        if (IsWindowVisible)
        {
            window.Hide();
        }
        else
        {
            window.Show();
        }
        IsWindowVisible = !IsWindowVisible;
    }

    [RelayCommand]
    public void ExitApplication()
    {
        Application.Current?.Quit();
    }
}