namespace E5Renewer.Models.Secrets
{
    /// <summary>The api interface for getting secrets.</summary>
    public interface ISecretProvider
    {
        /// <summary>Get password for certificate.</summary>
        /// <returns>The password of the certificate, <code>null</code> if not found.</returns>
        public Task<string?> GetPasswordForCertificateAsync(FileInfo certificate);

        /// <summary>Generate runtime token for authentication.</summary>
        /// <returns>The token.</returns>
        public Task<string> GetRuntimeTokenAsync();

        /// <summary>Get user secrets from file.</summary>
        /// <returns>A series of <see cref="UserSecret"/> loaded from file.</returns>
        /// <exception cref="InvalidDataException">Thorw if no loader can be found to load secret.</exception>
        public Task<UserSecret> GetUserSecretAsync();
    }
}
