using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeleSider.Pages;
using Backend;

namespace TeleSider.ViewModels;

public partial class _2FAPageViewModel : ObservableObject
{

    [ObservableProperty]
    private string password = null;

    [ObservableProperty]
    private string submitButtonText = "Submit";

    [RelayCommand]
    private async Task SubmitButtonPressed()
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
            if (await ConnectionManager.IsConnected())
            {
                try
                {
                    SetSubmitButtonText(true);
                    await Client.DoLogin(Password, null);
                    await Shell.Current.GoToAsync(nameof(HomePage));
                }
                catch
                {
                    await Shell.Current.DisplayAlert("Invalid password", "Please, try entering your password again", "Ok", "Cancel", FlowDirection.LeftToRight);
                }
                finally
                {
                    SetSubmitButtonText();
                }
            }
        }
    }

    [RelayCommand]
    private async Task BackButtonPressed()
    {
        bool request = await Shell.Current.DisplayAlert("Navigating back", "Do you want to abort the login operation?", "Yes", "Cancel", FlowDirection.LeftToRight);
        if (request)
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
    private void SetSubmitButtonText(bool isloading = false)
    {
        SubmitButtonText = isloading ? "Loading..." : "Submit";
    }
}