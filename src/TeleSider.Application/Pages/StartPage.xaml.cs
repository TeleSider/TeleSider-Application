using TeleSider.ViewModels;

namespace TeleSider.Pages;	

public partial class StartPage : ContentPage
{
    public StartPage(StartPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}