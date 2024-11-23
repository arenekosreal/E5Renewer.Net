namespace E5Renewer.Models.Statistics
{
    /// <summary>Generate a valid unix timestamp for handling network communication.</summary>
    public class UnixTimestampGenerator : IUnixTimestampGenerator
    {
        /// <inheritdoc/>
        public long GetUnixTimestamp()
        {
            return (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
        }
    }
}
