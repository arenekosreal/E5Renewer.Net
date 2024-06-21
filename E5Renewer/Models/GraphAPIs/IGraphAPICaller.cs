using E5Renewer.Models.Config;
using E5Renewer.Models.Modules;

namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>The api interface of msgraph apis callers.</summary>
    public interface IGraphAPICaller : IAspNetModule
    {
        /// <summary>Call next api for user.</summary>
        /// <param name="user">The user to call api.</param>
        public Task CallNextAPIAsync(GraphUser user);

        /// <summary>Wait for some time after one msgraph api is called.</summary>
        /// <param name="token">The token to cancel calming down.</param>
        /// <param name="user">The user to calm down.</param>
        public Task CalmDownAsync(CancellationToken token, GraphUser user);
    }
}
