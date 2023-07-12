﻿using CommunityToolkit.Maui;
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
        builder.Services.AddSingleton<StartPage>();
        builder.Services.AddSingleton<StartPageViewModel>();
        builder.Services.AddScoped<StartPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
