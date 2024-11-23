using Microsoft.AspNetCore.Http;

namespace E5Renewer.Controllers.V1;

internal static class IDummyResultGeneratorExtends
{
    public static async ValueTask<T> GenerateDummyResultAsync<T>(this IDummyResultGenerator generator, HttpContext context)
        where T : IJsonResponse
    {
        IJsonResponse response = await generator.GenerateDummyResultAsync(context);
        if (response is T typedResponse)
        {
            return typedResponse;
        }
        throw new NullReferenceException(nameof(response));
    }

    public static T GenerateDummyResult<T>(this IDummyResultGenerator generator, HttpContext context) where T : IJsonResponse =>
        generator.GenerateDummyResultAsync<T>(context).Result;
}
