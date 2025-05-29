namespace StatusSwift.Controls;

public partial class StatusIndicatorControl : ContentView
{
    public static readonly BindableProperty IsActiveProperty =
        BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(StatusIndicatorControl), false,
            propertyChanged: OnIsActiveChanged);

    public static readonly BindableProperty StatusTextProperty =
        BindableProperty.Create(nameof(StatusText), typeof(string), typeof(StatusIndicatorControl),
            "System Activity Disabled");

    public static readonly BindableProperty IndicatorColorProperty =
        BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(StatusIndicatorControl), Colors.Gray);

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

    public Color IndicatorColor
    {
        get => (Color)GetValue(IndicatorColorProperty);
        set => SetValue(IndicatorColorProperty, value);
    }

    private static void OnIsActiveChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is StatusIndicatorControl control && newValue is bool isActive)
        {
            control.StatusText = isActive ? "System Activity Enabled" : "System Activity Disabled";
            control.IndicatorColor = isActive ? Colors.LimeGreen : Colors.Gray;

            // Start pulsing animation for active state
            if (isActive) control.StartPulsingAnimation();
        }
    }

    private async void StartPulsingAnimation()
    {
        var indicator = this.FindByName<Border>("StatusIndicator");
        if (indicator != null && IsActive)
            // Continuous pulsing while active
            while (IsActive)
            {
                await indicator.FadeTo(0.3, 500, Easing.SinInOut);
                await indicator.FadeTo(1.0, 500, Easing.SinInOut);
            }
    }
}