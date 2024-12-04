using System.Runtime.Versioning;
using System.Security.AccessControl;

[SupportedOSPlatform("windows")]
internal static class UnixFileModeExtends
{
    private const FileSystemRights defaultListenUnixSocketPermissionEachRoleOnWindows =
        FileSystemRights.Read | FileSystemRights.Write;

    public static FileSystemRights ToUserFileSystemRights(
        this UnixFileMode mode,
        FileSystemRights defaultValue = defaultListenUnixSocketPermissionEachRoleOnWindows)
    {
        Dictionary<UnixFileMode, FileSystemRights> userModeMap = new()
            {
                {UnixFileMode.UserRead, FileSystemRights.Read},
                {UnixFileMode.UserWrite, FileSystemRights.Write},
                {UnixFileMode.UserExecute, FileSystemRights.ExecuteFile},
            };
        return mode.ToFileSystemRights(defaultValue, userModeMap);
    }

    public static FileSystemRights ToGroupFileSystemRights(
        this UnixFileMode mode,
        FileSystemRights defaultValue = defaultListenUnixSocketPermissionEachRoleOnWindows)
    {
        Dictionary<UnixFileMode, FileSystemRights> groupModeMap = new()
            {
                {UnixFileMode.GroupRead, FileSystemRights.Read},
                {UnixFileMode.GroupWrite, FileSystemRights.Write},
                {UnixFileMode.GroupExecute, FileSystemRights.ExecuteFile}
            };
        return mode.ToFileSystemRights(defaultValue, groupModeMap);
    }

    public static FileSystemRights ToOtherFileSystemRights(
        this UnixFileMode mode,
        FileSystemRights defaultValue = defaultListenUnixSocketPermissionEachRoleOnWindows
    )
    {
        Dictionary<UnixFileMode, FileSystemRights> otherModeMap = new()
            {
                {UnixFileMode.OtherRead, FileSystemRights.Read},
                {UnixFileMode.OtherWrite, FileSystemRights.Write},
                {UnixFileMode.OtherExecute, FileSystemRights.ExecuteFile}
            };
        return mode.ToFileSystemRights(defaultValue, otherModeMap);
    }

    private static FileSystemRights ToFileSystemRights(
        this UnixFileMode mode,
        FileSystemRights defaultValue,
        IEnumerable<KeyValuePair<UnixFileMode, FileSystemRights>> convertMap
    )
    {
        FileSystemRights ret = defaultValue;
        foreach (KeyValuePair<UnixFileMode, FileSystemRights> kv in convertMap)
        {
            if (mode.HasFlag(kv.Key) && !ret.HasFlag(kv.Value))
            {
                ret &= kv.Value;
            }
            else if (!mode.HasFlag(kv.Key) && ret.HasFlag(kv.Value))
            {
                ret &= ~kv.Value;
            }
        }
        return ret;
    }
}
