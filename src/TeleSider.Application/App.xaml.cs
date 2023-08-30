﻿using Backend;
using TeleSider.Pages;

namespace TeleSider;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
        Client.SetSessionPath(DeviceInfo.Current.Platform.ToString());
    }

    protected override void OnStart()
    {
        var task = InitAsync();

        task.ContinueWith((task) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MainPage = new AppShell();
                ShellNavigation.ClearNavigationStack();
                if (Client.isLoggedIn)
                {
                    Shell.Current.GoToAsync(nameof(HomePage));
                }
                else
                {
                    Shell.Current.GoToAsync(nameof(StartPage));
                }
            });
        });

        base.OnStart();
    }

    private async Task InitAsync()
    {
        await Client.ResumeSession();
    }
}
