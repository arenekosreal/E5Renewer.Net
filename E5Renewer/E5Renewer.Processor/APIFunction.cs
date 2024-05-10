using Microsoft.Graph;

namespace E5Renewer.Processor
{
    /// <summary>The delegate which accepts <c>GraphServiceClient</c> as input and returns <c>Task&lt;APICallResult&gt;</c>.</summary>
    /// <seealso cref="GraphServiceClient"/>
    /// <seealso cref="APICallResult"/>
    public delegate Task<APICallResult> APIFunction(GraphServiceClient client);
}
