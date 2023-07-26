using Microsoft.Maui.ApplicationModel;
using TeleSider;

public static class PermissionManager
{
    public static async Task<PermissionStatus> CheckAndRequestReadWrite()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<ReadWriteStoragePermission>();
        if (status == PermissionStatus.Granted)
            return status;
        if (Permissions.ShouldShowRationale<ReadWriteStoragePermission>())
        {
            await Shell.Current.DisplayAlert("Permission is required", "TeleSider uses this permission to work. Please, grant it", "Ok");
        }
        
        status = await Permissions.RequestAsync<ReadWriteStoragePermission>();

        return status;
    }
}

