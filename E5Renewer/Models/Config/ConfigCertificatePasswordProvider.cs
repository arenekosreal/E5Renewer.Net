using System.Security.Cryptography;

namespace E5Renewer.Models.Config
{
    /// <summary>Get password for a certificate from config.</summary>
    public class ConfigCertificatePasswordProvider : ICertificatePasswordProvider
    {
        private readonly ILogger<ConfigCertificatePasswordProvider> logger;
        private readonly Dictionary<string, string?>? passwords;
        private readonly Dictionary<string, string?> cache = new();

        /// <summary>Initialize <c>ConfigCertificatePasswordProvider</c> with parameters given.</summary>
        /// <param name="logger">The logger to create log.</param>
        /// <param name="passwords">The passwords of certificates. Defaults to <c>null</c></param>
        /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
        public ConfigCertificatePasswordProvider(
            ILogger<ConfigCertificatePasswordProvider> logger,
            Dictionary<string, string?>? passwords = null
        )
        {
            this.passwords = passwords;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<string?> GetPasswordForCertificateAsync(string certificate)
        {
            if (!this.cache.ContainsKey(certificate))
            {
                if (File.Exists(certificate))
                {
                    byte[] sha512Hash;
                    using (Stream stream = File.OpenRead(certificate))
                    {
                        sha512Hash = await SHA512.HashDataAsync(stream);
                    }
                    string fileHash = BitConverter.ToString(sha512Hash).Replace("-", "").ToLower();
                    this.logger.LogDebug("File hash for {0} is {1}", certificate, fileHash);
                    this.cache[certificate] =
                        this.passwords is Dictionary<string, string?> passwordsDict && passwordsDict.ContainsKey(fileHash)
                            ? passwordsDict[fileHash]
                            : null
                    ;
                }
            }
            return this.cache[certificate];
        }
    }
}
