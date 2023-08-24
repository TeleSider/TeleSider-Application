using TeleSider.ViewModels;

namespace TeleSider.Pages;

public partial class LoadingPage : ContentPage
{
    public LoadingPage(LoadingPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}