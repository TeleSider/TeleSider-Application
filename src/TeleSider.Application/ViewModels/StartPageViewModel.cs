using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeleSider.Pages;
using BackEnd;
using System.Diagnostics;

namespace TeleSider.ViewModels;

public partial class StartPageViewModel : ObservableObject
{
    [ObservableProperty]
    public string fullPhoneNumber = null;

    [ObservableProperty]
    public string countryCode = null;

    [ObservableProperty]
    public string phoneNumber = null;

    [RelayCommand]
    public async Task TapSignUp(string url) => await Launcher.OpenAsync(url);

    [RelayCommand]
    public async Task SignInButtonPressed()
    {
        if (!String.IsNullOrWhiteSpace(CountryCode) && !String.IsNullOrWhiteSpace(PhoneNumber)) {
            CountryCode = CountryCode.Replace(" ", "");
            PhoneNumber = PhoneNumber.Replace(" ", "");
            if (CountryCode.Length > 0 && PhoneNumber.Length >= 6)
            {
                FullPhoneNumber = CountryCode + PhoneNumber;
                if (FullPhoneNumber.All(Char.IsDigit))
                {
                    bool request = await Shell.Current.DisplayAlert("Is this the correct number?", $"+{FullPhoneNumber}", "Yes", "Edit", FlowDirection.LeftToRight);
                    if (request)
                    {
                        // Asking the user to grant the required permission, if they won't - close the app
                        await PermissionManager.CheckAndRequestReadWrite();
                        try
                            {
                                await Client.Login($"+{FullPhoneNumber}");
                                await NavigateToNumberVerificationPage();
                            }
                            catch (Exception ex) 
                            {
                                Debug.WriteLine(ex);
                                await DisplayInvalidPhoneNumberAlert("Something went wrong while trying to sign you in. Please, try again");
                                // remove a session 
                            }
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
    [RelayCommand]
    private async Task NavigateToNumberVerificationPage() => await Shell.Current.GoToAsync($"{nameof(PhoneVerificationPage)}?PhoneNumber=%2b{FullPhoneNumber}");

    private async Task DisplayInvalidPhoneNumberAlert(string details)
    {
        await Shell.Current.DisplayAlert("Invalid phone number", details, "Ok", "Cancel", FlowDirection.LeftToRight);
    }
}