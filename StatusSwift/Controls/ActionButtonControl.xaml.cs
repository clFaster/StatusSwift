using System.Windows.Input;

namespace StatusSwift.Controls;

public partial class ActionButtonControl : ContentView
{
    public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(ActionButtonControl), "Action");

    public static readonly BindableProperty ButtonCommandProperty =
        BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(ActionButtonControl));

    public static readonly BindableProperty ButtonBackgroundColorProperty =
        BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(Color), typeof(ActionButtonControl), Colors.Blue);

    public static readonly BindableProperty IsActiveStateProperty =
        BindableProperty.Create(nameof(IsActiveState), typeof(bool), typeof(ActionButtonControl), false,
            propertyChanged: OnIsActiveStateChanged);

    public ActionButtonControl()
    {
        InitializeComponent();

        // Setup click animation
        var button = this.FindByName<Button>("ToggleButton");
        if (button != null) button.Clicked += OnButtonClicked;

        // Initialize default styling
        UpdateButtonStyling(false); // Default to inactive state
    }

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }

    public Color ButtonBackgroundColor
    {
        get => (Color)GetValue(ButtonBackgroundColorProperty);
        set => SetValue(ButtonBackgroundColorProperty, value);
    }

    public bool IsActiveState
    {
        get => (bool)GetValue(IsActiveStateProperty);
        set => SetValue(IsActiveStateProperty, value);
    }

    private static void OnIsActiveStateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ActionButtonControl control && newValue is bool isActive) control.UpdateButtonStyling(isActive);
    }

    private void UpdateButtonStyling(bool isActive)
    {
        ButtonText = isActive ? "Disable StatusSwift" : "Enable StatusSwift";

        // Update button color based on state
        if (isActive)
        {
            // Red/Tertiary color for "Stop" button
            if (Application.Current?.Resources != null &&
                Application.Current.Resources.TryGetValue("Tertiary", out var stopColor))
                ButtonBackgroundColor = (Color)stopColor;
            else
                ButtonBackgroundColor = Colors.Red;
        }
        else
        {
            // Primary color for "Start" button
            if (Application.Current?.Resources != null &&
                Application.Current.Resources.TryGetValue("Primary", out var startColor))
                ButtonBackgroundColor = (Color)startColor;
            else
                ButtonBackgroundColor = Colors.Blue;
        }
    }

    private async void OnButtonClicked(object? sender, EventArgs e)
    {
        // Add button press animation
        var button = sender as Button;
        if (button != null)
        {
            await button.ScaleTo(0.95, 50, Easing.SinOut);
            await button.ScaleTo(1.0, 50, Easing.SinOut);
        }
    }
}