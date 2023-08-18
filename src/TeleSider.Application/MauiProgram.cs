using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TeleSider.Pages;
using TeleSider.ViewModels;

namespace TeleSider;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
		{
	                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		});
                
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomePageViewModel>();

        builder.Services.AddTransient<StartPage>();
        builder.Services.AddTransient<PhoneVerificationPage>();
        builder.Services.AddTransient<_2FAPage>();
        builder.Services.AddTransient<StartPageViewModel>();
        builder.Services.AddTransient<PhoneVerificationPageViewModel>();
        builder.Services.AddTransient<_2FAPageViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
	}
}
