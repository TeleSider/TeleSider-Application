using Microsoft.Maui.ApplicationModel;
using TeleSider;

public static class PermissionManager
{
    public static async Task<PermissionStatus> CheckAndRequestReadWrite()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<ReadWriteStoragePermission>();
        if (status == PermissionStatus.Granted)
            return status;
        status = await Permissions.RequestAsync<ReadWriteStoragePermission>();
        if (status != PermissionStatus.Granted)
        {
            var answer = await Shell.Current.DisplayAlert("Permission is required", "TeleSider uses this permission to work. Please, grant it", "Grant", "Quit");
            if (answer)
            {
                status = await Permissions.RequestAsync<ReadWriteStoragePermission>();
                if (status != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlert("Permission is required", "Sorry, " +
                        "you did not grant the required permission. If you want to grant it later - " +
                        """Go to "Settings -> Apps -> Permissions" and allow all required permissions. Now the application will close itself""", "OK");
                    App.Current.Quit();
                }
            }
            else
            {
                App.Current.Quit();
            }
        }
        return status;
    }
}

