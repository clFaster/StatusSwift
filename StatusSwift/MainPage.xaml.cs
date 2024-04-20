using StatusSwift.ViewModel;

namespace StatusSwift;

public partial class MainPage
{
    public MainPage(MainViewModel mainViewModel)
    {
        BindingContext = mainViewModel;
        InitializeComponent();
    }
}

