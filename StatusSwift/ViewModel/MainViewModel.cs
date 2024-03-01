using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StatusSwift.ViewModel;

public partial class MainViewModel: ObservableObject
{
    private static bool _statusSwiftActive;

    [ObservableProperty]
    private string _buttonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";

    [RelayCommand]
    private void ToggleStatusSwiftActive()
    {
        _statusSwiftActive = !_statusSwiftActive;
        ButtonText = _statusSwiftActive ? "Disable StatusSwift" : "Enable StatusSwift";
    }
}