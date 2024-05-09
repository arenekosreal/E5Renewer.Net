using E5Renewer.Modules;

namespace E5Renewer.Config
{
    public interface IConfigParser : IModule
    {
        public bool IsSupported(string path);
        public Task<RuntimeConfig> ParseConfig(string path);
    }
}
