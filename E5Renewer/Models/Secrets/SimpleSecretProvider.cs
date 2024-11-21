using System.Collections.Immutable;
using System.Security.Cryptography;

namespace E5Renewer.Models.Secrets
{
    /// <summary>Simple implementation of <see cref="ISecretProvider"/>.</summary>
    public class SimpleSecretProvider : ISecretProvider
    {
        private readonly FileInfo userSecret;
        private readonly ILogger<SimpleSecretProvider> logger;
        private readonly IEnumerable<IUserSecretLoader> secretLoaders;
        private readonly TokenOverride? tokenOverride;
        private readonly string randomGeneratedToken;
        private readonly Dictionary<FileInfo, string?> certificatePasswordsCache = new();
        private bool notifyRandom = true;
        private UserSecret? secretCache = null;

        /// <summary>Initialize <see cref="SimpleSecretProvider"/> with parameters given.</summary>
        /// <param name="logger">The logger to create logs.</param>
        /// <param name="userSecret">The file contains user secret info.</param>
        /// <param name="secretLoaders">The <see cref="IUserSecretLoader"/> implementation.</param>
        /// <param name="tokenOverride">The <see cref="TokenOverride"/> instance.</param>
        /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
        public SimpleSecretProvider(
            ILogger<SimpleSecretProvider> logger,
            [FromKeyedServices(nameof(userSecret))] FileInfo userSecret,
            IEnumerable<IUserSecretLoader> secretLoaders,
            TokenOverride? tokenOverride = null
        )
        {
            this.logger = logger;
            this.userSecret = userSecret;
            this.secretLoaders = secretLoaders;
            this.tokenOverride = tokenOverride;

            const int tokenSize = 32;
            const string tokenSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ0123456789";
            Random random = new();
            this.randomGeneratedToken = "";
            for (int i = 0; i < tokenSize; i++)
            {
                this.randomGeneratedToken += tokenSource[random.Next(tokenSource.Length)];
            }
        }

        /// <inheritdoc/>
        public async Task<string?> GetPasswordForCertificateAsync(FileInfo certificate)
        {
            if (this.certificatePasswordsCache.ContainsKey(certificate))
            {
                return this.certificatePasswordsCache[certificate];
            }
            else if (certificate.Exists)
            {
                ImmutableDictionary<string, string> passwords =
                    (await this.GetUserSecretAsync()).passwords ?? ImmutableDictionary.Create<string, string>();
                byte[] sha512Hash;
                using (Stream stream = certificate.OpenRead())
                {
                    sha512Hash = await SHA512.HashDataAsync(stream);
                }
                string fileHash = BitConverter.ToString(sha512Hash).Replace("-", "").ToLower();
                this.logger.LogDebug("File hash for {0} is {1}", certificate, fileHash);
                this.certificatePasswordsCache[certificate] =
                    passwords.ContainsKey(fileHash)
                        ? passwords[fileHash]
                        : null
                ;
                return this.certificatePasswordsCache[certificate];
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<string> GetRuntimeTokenAsync()
        {
            string? token = this.tokenOverride?.token;
            FileInfo? tokenFile = this.tokenOverride?.tokenFile;

            if (token is not null)
            {
                return token;
            }

            if (tokenFile is not null)
            {
                using (StreamReader stream = tokenFile.OpenText())
                {
                    string? line = await stream.ReadLineAsync();
                    if (line is not null)
                    {
                        return line.Trim();
                    }
                }
            }
            if (this.notifyRandom)
            {
                this.logger.LogWarning("Using random generated token {0}", this.randomGeneratedToken);
                this.notifyRandom = false;
            }
            return this.randomGeneratedToken;
        }

        /// <inheritdoc/>
        public async Task<UserSecret> GetUserSecretAsync()
        {
            if (this.secretCache is UserSecret secretCache)
            {
                return secretCache;
            }
            try
            {
                IUserSecretLoader loader = this.secretLoaders.First((l) => l.IsSupported(this.userSecret));
                this.logger.LogDebug("Using {0} to load user secret {1}.", loader.name, this.userSecret);
                UserSecret secretResult = await loader.LoadSecretAsync(this.userSecret);
                this.secretCache = secretResult;
                return secretResult;
            }
            catch (InvalidOperationException ioe)
            {
                this.logger.LogError("Failed to find {0} for user secret from because {1}.", nameof(IUserSecretLoader), ioe.Message);
            }
            catch (Exception e)
            {
                this.logger.LogError("Failed to load user secret from because {0}.", e.Message);
            }
            throw new InvalidDataException($"Cannot load {this.userSecret}.");
        }
    }
}
