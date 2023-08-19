public class ReadWriteStoragePermission : Permissions.BasePlatformPermission
{
#if ANDROID
    public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
        new List<(string androidPermission, bool isRuntime)>
        {
        (global::Android.Manifest.Permission.ReadExternalStorage, true),
        (global::Android.Manifest.Permission.WriteExternalStorage, true)
        }.ToArray();
#endif
}