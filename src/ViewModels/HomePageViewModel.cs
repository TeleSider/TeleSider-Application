using BackEnd;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TeleSider.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    public string username = Client.username;
}