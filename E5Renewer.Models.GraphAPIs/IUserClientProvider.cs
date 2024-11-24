using E5Renewer.Models.Secrets;

using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs;

/// <summary>The api interface to keep <see cref="GraphServiceClient"/> for <see cref="User"/></summary>
public interface IUserClientProvider
{
    /// <summary>Get client for user.</summary>
    public Task<GraphServiceClient> GetClientForUserAsync(User user);
}
