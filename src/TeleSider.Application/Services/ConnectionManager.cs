namespace TeleSider;
public static class ConnectionManager
{
    public static async Task<bool> IsConnected()
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
}
