using TeleSider.ViewModels;

namespace TeleSider.Pages;

public partial class PhoneVerificationPage : ContentPage
{
	public PhoneVerificationPage(PhoneVerificationPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}