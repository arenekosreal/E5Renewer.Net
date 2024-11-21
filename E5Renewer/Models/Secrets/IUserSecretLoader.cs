using E5Renewer.Models.Modules;

namespace E5Renewer.Models.Secrets
{
    /// <summary> The api interface for loading user secret.</summary>
    public interface IUserSecretLoader : IModule
    {
        /// <summary>If file given is supported by the loader.</summary>
        public bool IsSupported(FileInfo secretFile);

        /// <summary>Load a series of <see cref="UserSecret"/> from file given.</summary>
        public Task<UserSecret> LoadSecretAsync(FileInfo secretFile);
    }
}
