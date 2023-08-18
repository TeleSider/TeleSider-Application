using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeleSider.Pages;
using Backend;
using System.Diagnostics;

namespace TeleSider.ViewModels;

public partial class StartPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string signInButtonText = "Sign In";

    [ObservableProperty]
    private string phoneNumber = null;

    [RelayCommand]
    private async Task TapSignUp(string url) => await Launcher.OpenAsync(url);

    [RelayCommand]
    private async Task SignInButtonPressed()
    {
#if ANDROID
        Platforms.KeyboardManager.HideKeyboard();
#endif
        if (!String.IsNullOrWhiteSpace(PhoneNumber))
        {
            PhoneNumber = PhoneNumber.Replace(" ", "");
            if (PhoneNumber.Length >= 7)
            {
                if (PhoneNumber.All(Char.IsDigit))
                {
                    bool request = await Shell.Current.DisplayAlert("Is this the correct number?", $"+{PhoneNumber}", "Yes", "Edit", FlowDirection.LeftToRight);
                    if (request)
                    {
                        // Asking the user to grant the required permission, if they won't - close the app
                        await PermissionManager.CheckAndRequestReadWrite();
                        if (await IsConnected())
                        try
                        {
                            SetSignInButtonText(true);
                            await Client.Login($"+{PhoneNumber}");
                            await NavigateToNumberVerificationPage();
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                                Debug.WriteLine($"Exception {ex}");
#endif
                                await Shell.Current.DisplayAlert("Something went wrong while trying to sign you in",
                                                            "Make sure the phone number is correct, check your connection and try again",
                                                            "Ok", FlowDirection.LeftToRight);
                        }
                        finally
                        {
                            SetSignInButtonText();
                        }
                    }
                    else
                    {
                        SetSignInButtonText();
                    }
                }
                else
                {
                    await DisplayInvalidPhoneNumberAlert("Please, try again");
                }
            }
            else
            {
                await DisplayInvalidPhoneNumberAlert("Please, make sure the phone number is correct and try again");
            }
        }
        else
        {
            await DisplayInvalidPhoneNumberAlert("Please, fill in all required fields");
        }
    }
    // %2b means "+" in url, it is the only way to pass the phone number with a "+" sign
    private async Task NavigateToNumberVerificationPage() => await Shell.Current.GoToAsync($"{nameof(PhoneVerificationPage)}?PhoneNumber=%2b{PhoneNumber}");

    private async Task DisplayInvalidPhoneNumberAlert(string details)
    {
        await Shell.Current.DisplayAlert("Invalid phone number", details, "Ok", FlowDirection.LeftToRight);
    }
    [RelayCommand]
    private async Task LoginTheExistingUser()
    {
        SetSignInButtonText(true);
        if (await Client.ResumeSession())
        {
#if ANDROID
            Platforms.KeyboardManager.HideKeyboard();
#endif
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
        SetSignInButtonText();
    }
    private async Task<bool> IsConnected()
    {
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            return true;
        }
        else
        {
            await Shell.Current.DisplayAlert("No Internet Access", "Please, check your connection and try again", "Ok", FlowDirection.LeftToRight);
            return false;
        }
    }
    private void SetSignInButtonText(bool isloading=false)
    {
        SignInButtonText = isloading ? "Loading..." : "Sign In";
    }
}