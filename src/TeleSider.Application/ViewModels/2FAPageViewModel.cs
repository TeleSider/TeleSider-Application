﻿using System.Diagnostics;
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
        if (String.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Invalid password", "Please, try entering your password again", "Ok", "Cancel", FlowDirection.LeftToRight);
        }
        else
        {
            try
            {
                await Client.DoLogin(Password);
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Invalid password", "Please, try entering your password again", "Ok", "Cancel", FlowDirection.LeftToRight);
            }
        }
    }
}