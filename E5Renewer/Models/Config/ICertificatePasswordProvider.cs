namespace E5Renewer.Models.Config
{
    /// <summary>The api interface for getting password for a certificate.</summary>
    public interface ICertificatePasswordProvider
    {
        /// <summary>Get password for a certificate.</summary>
        /// <returns>The password for the certificate given. <code>null</code> if no password.</returns>
        public Task<string?> GetPasswordForCertificateAsync(string certificate);
    }
}
