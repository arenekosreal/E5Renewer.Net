namespace E5Renewer.Statistics
{
    /// <summary>Utils for handling network requests.</summary>
    public static class Helper
    {
        /// <summary>Get milliseconded unix timestamp.</summary>
        /// <returns>The timestamp.</returns>
        public static long GetUnixTimestamp()
        {
            return (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
        }
    }
}
