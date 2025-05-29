using StatusSwift.ViewModel;

namespace StatusSwift;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    private bool _isAnimating = false;

    public MainPage(MainViewModel mainViewModel)
    {
        BindingContext = mainViewModel;
        _viewModel = mainViewModel;
        InitializeComponent();
        
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        
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
        };
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
                
                await Task.Delay(400);
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

