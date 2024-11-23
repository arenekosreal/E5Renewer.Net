using Microsoft.AspNetCore.Http;

namespace E5Renewer.Controllers;

/// <summary>The api interface to generate dummy response.</summary>
public interface IDummyResultGenerator
{
    /// <summary>Generate a dummy result when something not right.</summary>
    public Task<IJsonResponse> GenerateDummyResultAsync(HttpContext httpContext);

    /// <summary>Generate a dummy result when something not right.</summary>
    public IJsonResponse GenerateDummyResult(HttpContext httpContext);
}

