namespace E5Renewer.Statistics
{
    public static class Helper
    {
        public static long GetUnixTimestamp()
        {
            return (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
        }
    }
}
