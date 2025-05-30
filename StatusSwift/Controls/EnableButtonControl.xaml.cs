using System.Windows.Input;

namespace StatusSwift.Controls;

public partial class EnableButtonControl : ContentView
{
    public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(EnableButtonControl), "Enabled");

    public static readonly BindableProperty ButtonCommandProperty =
        BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(EnableButtonControl));

    public EnableButtonControl()
    {
        InitializeComponent();

        // Setup click animation
        var button = this.FindByName<Button>("EnableButton");
        if (button != null) button.Clicked += OnButtonClicked;
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
