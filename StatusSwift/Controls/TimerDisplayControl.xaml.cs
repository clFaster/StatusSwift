namespace StatusSwift.Controls;

public partial class TimerDisplayControl : ContentView
{
    public static readonly BindableProperty TimerVisibleProperty =
        BindableProperty.Create(nameof(TimerVisible), typeof(bool), typeof(TimerDisplayControl), false);

    public static readonly BindableProperty ElapsedTimeProperty =
        BindableProperty.Create(nameof(ElapsedTime), typeof(string), typeof(TimerDisplayControl), "00:00:00");

    public static readonly BindableProperty TimerLabelProperty =
        BindableProperty.Create(nameof(TimerLabel), typeof(string), typeof(TimerDisplayControl), "Running Time");

    public TimerDisplayControl()
    {
        InitializeComponent();
    }

    public bool TimerVisible
    {
        get => (bool)GetValue(TimerVisibleProperty);
        set => SetValue(TimerVisibleProperty, value);
    }

    public string ElapsedTime
    {
        get => (string)GetValue(ElapsedTimeProperty);
        set => SetValue(ElapsedTimeProperty, value);
    }

    public string TimerLabel
    {
        get => (string)GetValue(TimerLabelProperty);
        set => SetValue(TimerLabelProperty, value);
    }
}