﻿using Backend;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TeleSider.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    private string username = Client.GetUsername();
}