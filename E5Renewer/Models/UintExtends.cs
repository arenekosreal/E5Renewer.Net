namespace E5Renewer.Models
{
    /// <summary>Extended methods to
    /// <see cref="uint">uint</see>.
    /// </summary>
    public static class UintExtends
    {
        private const uint minPermission = 0; // 0o000
        private const uint maxPermission = 511; // 0o777

        /// <summary>Convert
        /// <see cref="uint">uint</see>
        /// to
        /// <see cref="UnixFileMode">UnixFileMode</see>.
        /// </summary>
        public static UnixFileMode ToUnixFileMode(this uint permission)
        {

            if (permission < UintExtends.minPermission)
            {
                permission = UintExtends.minPermission;
            }

            if (permission > UintExtends.maxPermission)
            {
                permission = UintExtends.maxPermission;
            }

            return (UnixFileMode)permission;
        }
    }
}
