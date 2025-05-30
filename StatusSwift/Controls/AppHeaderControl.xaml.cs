namespace StatusSwift.Controls;

public partial class AppHeaderControl : ContentView
{
    public static readonly BindableProperty AppTitleProperty =
        BindableProperty.Create(nameof(AppTitle), typeof(string), typeof(AppHeaderControl), "StatusSwift");

    public static readonly BindableProperty AppSubtitleProperty =
        BindableProperty.Create(nameof(AppSubtitle), typeof(string), typeof(AppHeaderControl),
            "Keep your system active");

    public AppHeaderControl()
    {
        InitializeComponent();
    }

    public string AppTitle
    {
        get => (string)GetValue(AppTitleProperty);
        set => SetValue(AppTitleProperty, value);
    }

    public string AppSubtitle
    {
        get => (string)GetValue(AppSubtitleProperty);
        set => SetValue(AppSubtitleProperty, value);
    }
}