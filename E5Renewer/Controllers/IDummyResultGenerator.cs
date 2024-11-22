namespace E5Renewer.Controllers
{
    /// <summary>The api interface to generate dummy response.</summary>
    public interface IDummyResultGenerator
    {
        /// <summary>Generate a dummy result when something not right.</summary>
        public Task<JsonAPIV1Response> GenerateDummyResultAsync(HttpContext httpContext);

        /// <summary>Generate a dummy result when something not right.</summary>
        public JsonAPIV1Response GenerateDummyResult(HttpContext httpContext);
    }
}
