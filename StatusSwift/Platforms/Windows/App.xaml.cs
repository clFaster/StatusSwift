using H.NotifyIcon;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

// ReSharper disable once CheckNamespace
namespace StatusSwift.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();

        WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var nativeWindow = handler.PlatformView;
            var appWindow = nativeWindow.GetAppWindow();
            if (appWindow is not null)
                appWindow.Changed += (sender, args) =>
                {
                    if (appWindow.Presenter is not OverlappedPresenter overlappedPresenter) return;
                    if (!args.DidPresenterChange ||
                        overlappedPresenter.State != OverlappedPresenterState.Minimized) return;

                    if (view is Window window)
                    {
                        window.Hide();
                    }
                };
        });
    }

    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}