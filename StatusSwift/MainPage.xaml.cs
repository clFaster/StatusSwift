using StatusSwift.ViewModel;

namespace StatusSwift;

public partial class MainPage
{
    public MainPage(MainViewModel mainViewModel)
    {
        BindingContext = mainViewModel;
        InitializeComponent();
    }

    private void ToggleAutoStartActive(object? sender, ToggledEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ToggleAlternativeModeActive(object? sender, ToggledEventArgs e)
    {
        throw new NotImplementedException();
    }
}

