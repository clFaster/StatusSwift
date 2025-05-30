using System.Windows.Input;

namespace StatusSwift.Controls;

public partial class DisableStatusSwiftButtonControl
{
    public static readonly BindableProperty ButtonCommandProperty =
        BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(DisableStatusSwiftButtonControl));

    public DisableStatusSwiftButtonControl()
    {
        InitializeComponent();
    }

    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }
}