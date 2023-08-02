using System.Diagnostics;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeleSider.Pages;
using BackEnd;

namespace TeleSider.ViewModels;

public partial class _2FAPageViewModel : ObservableObject
{

    [ObservableProperty]
    public string password = null;

    [RelayCommand]
    public async Task SubmitButtonPressed()
    {
#if ANDROID
        Platforms.KeyboardManager.HideKeyboard();
#endif
        if (String.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Invalid password", "Please, try entering your password again", "Ok", "Cancel", FlowDirection.LeftToRight);
        }
        else
        {
            try
            {
                await Client.DoLogin(Password, null);
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Invalid password", "Please, try entering your password again", "Ok", "Cancel", FlowDirection.LeftToRight);
            }
        }
    }

    [RelayCommand]
    public async Task BackButtonPressed() => await Shell.Current.GoToAsync("../../");
}