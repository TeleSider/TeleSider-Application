using System.Diagnostics;
using WTelegram;

namespace Backend;
public static partial class Client
{
    private static WTelegram.Client? _client;


    private static readonly int apiID;
    private static readonly string apiHash;
    private static string? _sessionPath;

    public static bool isLoggedIn = false;

    private static bool _isExistingSessionChecked = false;


    public static async Task Login(string? phoneNumber=null)
    {
        DisposeClient();
        _client = new WTelegram.Client(Config);
        await DoLogin(phoneNumber, "verification_code");
    }

    // returns the required Item, null if the user is logged in
    public static async Task<string?> DoLogin(string? loginInfo, string? requiredItem)
    {
        while (_client.User == null)
            switch (await _client.Login(loginInfo))
            {
                case "verification_code": 
                {
                    if (requiredItem == "verification_code")
                        return requiredItem;
                    else
                        throw new Exception("Invalid Verification Code");
                }
                case "password": 
                {
                    if (requiredItem == "password")
                        return requiredItem;
                    else
                        throw new Exception("Invalid Password");
                }
                default:
                {
                    break;
                }
            }
        isLoggedIn = true;
        return null;
    }
    public static void SetSessionPath(string platform)
    {
        switch (platform)
        {
            case "Android":
                _sessionPath = "/data/data/com.telesider.telesider/files/TeleSider.session";
                break;
            case "WinUI":
                break;
            // Other platforms
            default:
                throw new Exception("The platform is unknown");
        }
    }
    // returns true if the current user is successfully logged in, otherwise returns false
    public static async Task<bool> ResumeSession()
    {
        if (!_isExistingSessionChecked)
        {
            CreateClientIfNeeded();
            try
            {
                await _client.LoginUserIfNeeded(null, false);
                isLoggedIn = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to resume the session");
                Debug.WriteLine(ex.Message);
            }
            _isExistingSessionChecked = true;
        }
        return isLoggedIn;
    }

    private static void CreateClientIfNeeded() => _client ??= new WTelegram.Client(Config);
    public static void DisposeClient() => _client?.Dispose();
    public static string? GetUsername()
    {
        if (_client != null && _client.User != null)
        {
            return _client.User.ToString();
        }
        else
        {
            return null;
        }
    }

    static string Config(string what)
    {
        switch (what)
        {
            case "api_id": return apiID.ToString();
            case "api_hash": return apiHash;
            case "session_pathname": return _sessionPath;
            case "user_id": return "-1";
            default: return null;
        }
    }

    private static void DebugLogger(int level, string message) => Debug.WriteLine(message);
}
