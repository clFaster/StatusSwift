using StatusSwift.ViewModel;

namespace StatusSwift;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel mainViewModel)
    {
        BindingContext = mainViewModel;
        _viewModel = mainViewModel;
        InitializeComponent();
        
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
}

