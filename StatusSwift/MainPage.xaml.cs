using StatusSwift.ViewModel;

namespace StatusSwift;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    private bool _isAnimating = false;    public MainPage(MainViewModel mainViewModel)
    {
        BindingContext = mainViewModel;
        _viewModel = mainViewModel;
        InitializeComponent();
        
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        
        SetupButtonAnimations();
        
        this.Loaded += (s, e) => 
        {
            if (Application.Current?.Windows.Count > 0)
            {
                var window = Application.Current.Windows[0];
                if (window != null)
                {
                    if (window.Width < 450)
                        window.Width = 450;
                    
                    if (window.Height < 500)
                        window.Height = 500;
                }
            }
        };    }    private void SetupButtonAnimations()
    {
        var pointerGestureRecognizer = new PointerGestureRecognizer();
        
        pointerGestureRecognizer.PointerEntered += async (s, e) =>
        {
            var originalColor = GetOriginalColor();
            var hoverColor = GetHoverColor(originalColor);

            await AnimateColorChange(originalColor, hoverColor, 200);
        };

        pointerGestureRecognizer.PointerExited += async (s, e) =>
        {
            var originalColor = GetOriginalColor();
            var currentColor = ToggleButton.BackgroundColor;
            
            await AnimateColorChange(currentColor, originalColor, 200);
        };

        ToggleButton.GestureRecognizers.Add(pointerGestureRecognizer);

        ToggleButton.Pressed += async (s, e) =>
        {
            var originalColor = GetOriginalColor();
            var pressedColor = GetPressedColor(originalColor);
            await AnimateColorChange(ToggleButton.BackgroundColor, pressedColor, 100);
        };

        ToggleButton.Released += async (s, e) =>
        {
            var originalColor = GetOriginalColor();
            await AnimateColorChange(ToggleButton.BackgroundColor, originalColor, 150);        };
    }

    private async Task AnimateColorChange(Color fromColor, Color toColor, uint duration)
    {
        var animation = new Animation(v =>
        {
            var r = (float)(fromColor.Red + (toColor.Red - fromColor.Red) * v);
            var g = (float)(fromColor.Green + (toColor.Green - fromColor.Green) * v);
            var b = (float)(fromColor.Blue + (toColor.Blue - fromColor.Blue) * v);
            var a = (float)(fromColor.Alpha + (toColor.Alpha - fromColor.Alpha) * v);
            
            ToggleButton.BackgroundColor = new Color(r, g, b, a);
        }, 0, 1);

        animation.Commit(ToggleButton, "ColorAnimation", 16, duration, Easing.CubicOut);
        await Task.Delay((int)duration);
    }

    private static Color GetHoverColor(Color baseColor)
    {
        baseColor.ToHsl(out var h, out var s, out var l);
        return Color.FromHsla(h, s, Math.Min(l + 0.15f, 1.0f), baseColor.Alpha);
    }

    private static Color GetPressedColor(Color baseColor)
    {
        baseColor.ToHsl(out var h, out var s, out var l);
        return Color.FromHsla(h, s, Math.Max(l - 0.2f, 0.0f), baseColor.Alpha);
    }

    private Color GetOriginalColor()
    {
        if (_viewModel.IsActive)
        {
            if (Application.Current?.Resources != null && 
                Application.Current.Resources.TryGetValue("Tertiary", out var stopColor))
            {
                return (Color)stopColor;
            }
            return Colors.Red;
        }
        else
        {
            if (Application.Current?.Resources != null && 
                Application.Current.Resources.TryGetValue("Primary", out var startColor))
            {
                return (Color)startColor;
            }
            return Colors.Blue;
        }
    }

    private async void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.IsActive))
        {
            if (_viewModel.IsActive)
            {
                await StartPulseAnimation();
            }
            else
            {
                StopPulseAnimation();
            }
        }
    }

    private async Task StartPulseAnimation()
    {
        if (_isAnimating) return;
        
        _isAnimating = true;
        
        try
        {
            while (_isAnimating && _viewModel.IsActive)
            {
                await StatusIndicator.ScaleTo(1.20, 250, Easing.CubicInOut);
                
                if (!_isAnimating || !_viewModel.IsActive) break;
                
                await StatusIndicator.ScaleTo(1.0, 250, Easing.CubicInOut);
                
                if (!_isAnimating || !_viewModel.IsActive) break;
                
                await Task.Delay(500);
            }
        }
        catch
        {
            // Animation cancelled
        }
        finally
        {
            try
            {
                await StatusIndicator.ScaleTo(1.0, 250);
            }
            catch
            {
            }
            _isAnimating = false;
        }
    }

    private void StopPulseAnimation()
    {
        _isAnimating = false;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
        StopPulseAnimation();
    }
}

