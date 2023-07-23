using System.Diagnostics;
using TL;
using WTelegram;

namespace BackEnd;
public class Client
{
    public static WTelegram.Client client;
    public static string username = "";
    public static async Task Login(string phoneNumber)
    {
        client = new WTelegram.Client(1, "1");
        await DoLogin(phoneNumber);
        //client.Dispose(); // the client must be disposed when you're done running your userbot.
    }

    public static async Task DoLogin(string loginInfo)
        {
            while (client.User == null)
                switch (await client.Login(loginInfo))
                {
                    case "verification_code": return;
                    case "password": return;
                    default: loginInfo = null; break;
                }
        username = client.User.ToString();
        }

}