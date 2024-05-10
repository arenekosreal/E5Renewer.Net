using E5Renewer.Modules;
using E5Renewer.Config;

namespace E5Renewer.Processor
{
    /// <summary> The api interface of APICaller.</summary>
    public interface IAPICaller : IModule
    {
        /// <summary>Call next APIFunction.</summary>
        /// <param name="update">If update result, defaults to <c>true</c></param>
        /// <seealso cref="APIFunction"/>
        public Task CallNextAPI(bool update = true);
        /// <summary>Add user to caller.</summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(GraphUser user);
    }
}
