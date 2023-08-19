using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeleSider.Pages;
using Backend;

namespace TeleSider.ViewModels;

[QueryProperty(nameof(PhoneNumber), "PhoneNumber")]
public partial class PhoneVerificationPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string verifyButtonText = "Verify";

    [ObservableProperty]
    private string verificationCode = null;

    [RelayCommand]
    private async Task SubmitButtonPressed()
    {
#if ANDROID
        Platforms.KeyboardManager.HideKeyboard();
#endif
        if (VerificationCode == null || VerificationCode.Length < 5 || !VerificationCode.All(Char.IsDigit))
        {
            await Shell.Current.DisplayAlert("Invalid verification code", "Please, check the verification code and try again", "Ok", "Cancel", FlowDirection.LeftToRight);
        }
        else
        {
            if (await ConnectionManager.IsConnected())
            {
                try
                {
                    SetVerifyButtonText(true);
                    string requiredItem = await Client.DoLogin(VerificationCode, "password");
                    if (requiredItem == "password") 
                    {
                        await Shell.Current.GoToAsync(nameof(_2FAPage));
                    }
                    else
                    {
                        await Shell.Current.GoToAsync(nameof(HomePage));
                    }
                }
                catch
                {
                    await Shell.Current.DisplayAlert("Invalid verification code", "Please, check the verification code and try again", "Ok", "Cancel", FlowDirection.LeftToRight);
                }
                finally
                {
                    SetVerifyButtonText();
                }
            }
        }
    }
    private void SetVerifyButtonText(bool isloading = false)
    {
        VerifyButtonText = isloading ? "Verifying..." : "Verify";
    }
}