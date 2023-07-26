using TeleSider.Pages;

namespace TeleSider;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(PhoneVerificationPage), typeof(PhoneVerificationPage));
        Routing.RegisterRoute(nameof(_2FAPage), typeof(_2FAPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    }
}
