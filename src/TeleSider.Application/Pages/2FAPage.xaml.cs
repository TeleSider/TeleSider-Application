using TeleSider.ViewModels;

namespace TeleSider.Pages;

public partial class _2FAPage : ContentPage
{
    public _2FAPage(_2FAPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}