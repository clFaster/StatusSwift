namespace StatusSwift.Controls;

public partial class StatusIndicatorControl : ContentView
{
    public static readonly BindableProperty IsActiveProperty =
        BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(StatusIndicatorControl), false,
            propertyChanged: OnIsActiveChanged);

    public static readonly BindableProperty StatusTextProperty =
        BindableProperty.Create(nameof(StatusText), typeof(string), typeof(StatusIndicatorControl),
            "System Activity Disabled");

    public StatusIndicatorControl()
    {
        InitializeComponent();
    }

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    public string StatusText
    {
        get => (string)GetValue(StatusTextProperty);
        set => SetValue(StatusTextProperty, value);
    }

    private static void OnIsActiveChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is StatusIndicatorControl control && newValue is bool isActive)
            control.StatusText = isActive ? "System Activity Enabled" : "System Activity Disabled";
    }
}