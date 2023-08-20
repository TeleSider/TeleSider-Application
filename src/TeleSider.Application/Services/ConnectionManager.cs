namespace TeleSider;
public static class ConnectionManager
{
    public static async Task<bool> IsConnected(bool displayAlert=true)
    {
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            return true;
        }
        else
        {
            if (displayAlert)
            {
                await Shell.Current.DisplayAlert("No Internet Access", "Please, check your connection and try again", "Ok", FlowDirection.LeftToRight);
            }
            return false;
        }
    }
}
