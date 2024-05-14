namespace E5Renewer.Models.Statistics
{
    /// <summary>The api interface for generating unix timestamps for handling network communications.</summary>
    public interface IUnixTimestampGenerator
    {
        /// <summary>Get milliseconded unix timestamp.</summary>
        /// <returns>The timestamp.</returns>
        public long GetUnixTimestamp();
    }
}
