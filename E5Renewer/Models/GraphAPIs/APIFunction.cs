using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>Delegate to call msgraph api.</summary>
    /// <param name="client">The <see cref="GraphServiceClient">GraphServiceClient</see> to call msgraph api.</param>
    /// <returns>The <see cref="Task">Task</see> which contains calling result <see cref="APICallResult">r=APICallResult</see>.</returns>
    public delegate Task<APICallResult> APIFunction(GraphServiceClient client);
}
