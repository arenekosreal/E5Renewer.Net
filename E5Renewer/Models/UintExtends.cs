namespace E5Renewer.Models
{
    /// <summary>Extended methods to
    /// <see cref="uint">uint</see>.
    /// </summary>
    public static class UintExtends
    {
        /// <summary>Convert
        /// <see cref="uint">uint</see>
        /// to
        /// <see cref="UnixFileMode">UnixFileMode</see>.
        /// </summary>
        public static UnixFileMode ToUnixFileMode(this uint permission)
        {
            if (permission > 777)
            {
                permission = 777;
            }
            uint user = permission / 100;
            uint group = (permission - user * 100) / 10;
            uint other = permission - user * 100 - group * 10;
            UnixFileMode userFileMode = user switch
            {
                0 => UnixFileMode.None,
                1 => UnixFileMode.UserExecute,
                2 => UnixFileMode.UserWrite,
                3 => UnixFileMode.UserWrite | UnixFileMode.UserExecute,
                4 => UnixFileMode.UserRead,
                5 => UnixFileMode.UserRead | UnixFileMode.UserExecute,
                6 => UnixFileMode.UserRead | UnixFileMode.UserWrite,
                7 => UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute,
                _ => throw new ArgumentOutOfRangeException(nameof(user), string.Format("Invalid unix user permission value {0}", user))
            };
            UnixFileMode groupFileMode = group switch
            {
                0 => UnixFileMode.None,
                1 => UnixFileMode.GroupExecute,
                2 => UnixFileMode.GroupWrite,
                3 => UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
                4 => UnixFileMode.GroupRead,
                5 => UnixFileMode.GroupRead | UnixFileMode.GroupExecute,
                6 => UnixFileMode.GroupRead | UnixFileMode.GroupWrite,
                7 => UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
                _ => throw new ArgumentOutOfRangeException(nameof(group), string.Format("Invalid unix group permission value {0}", group))
            };
            UnixFileMode otherFileMode = other switch
            {
                0 => UnixFileMode.None,
                1 => UnixFileMode.OtherExecute,
                2 => UnixFileMode.OtherWrite,
                3 => UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
                4 => UnixFileMode.OtherRead,
                5 => UnixFileMode.OtherRead | UnixFileMode.OtherExecute,
                6 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite,
                7 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
                _ => throw new ArgumentOutOfRangeException(nameof(other), string.Format("Invalid unix other permission {0}", other))
            };
            return userFileMode | groupFileMode | otherFileMode;
        }
    }
}
