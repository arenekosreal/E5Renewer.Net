using E5Renewer.Modules;
using E5Renewer.Config;

namespace E5Renewer.Processor
{
    public interface IAPICaller : IModule
    {
        public Task CallNextAPI(bool update = true);
        public void AddUser(GraphUser user);
    }
}
