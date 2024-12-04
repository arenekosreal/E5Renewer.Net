using System.Security.AccessControl;
using System.Security.Principal;

internal static class FileInfoExtends
{
    public static void SetUnixFileMode(this FileInfo fileInfo, UnixFileMode mode)
    {
        if (OperatingSystem.IsWindows())
        {
            // https://stackoverflow.com/a/54713612
            // Why Windows is sooo complicated?

            FileSecurity fileSecurity =
                fileInfo.GetAccessControl(AccessControlSections.Owner | AccessControlSections.Group | AccessControlSections.Access);
            IdentityReference? ownerIdentity = fileSecurity.GetOwner(typeof(NTAccount));
            IdentityReference? groupIdentity = fileSecurity.GetGroup(typeof(NTAccount));
            IdentityReference otherIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null)
                .Translate(typeof(NTAccount));

            if (ownerIdentity is not null)
            {
                fileSecurity.ModifyAccessRule(
                    AccessControlModification.Set,
                    new FileSystemAccessRule(ownerIdentity, mode.ToUserFileSystemRights(), AccessControlType.Allow),
                    out _
                );
            }

            if (groupIdentity is not null)
            {
                fileSecurity.ModifyAccessRule(
                    AccessControlModification.Set,
                    new FileSystemAccessRule(groupIdentity, mode.ToGroupFileSystemRights(), AccessControlType.Allow),
                    out _
                );
            }

            fileSecurity.ModifyAccessRule(
                AccessControlModification.Set,
                new FileSystemAccessRule(otherIdentity, mode.ToOtherFileSystemRights(), AccessControlType.Allow),
                out _
            );
        }
        else
        {
            File.SetUnixFileMode(fileInfo.FullName, mode);
        }
    }
}
