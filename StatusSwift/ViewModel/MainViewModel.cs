using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
}