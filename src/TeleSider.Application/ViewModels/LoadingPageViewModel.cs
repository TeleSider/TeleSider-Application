using Backend;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeleSider.Pages;

namespace TeleSider.ViewModels;

public partial class LoadingPageViewModel : ObservableObject
{
    [RelayCommand]
    private async Task ScreenTapped()
    {
        if (Client.isLoggedIn)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}