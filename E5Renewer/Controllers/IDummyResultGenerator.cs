namespace E5Renewer.Controllers
{
    /// <summary>The api interface to generate dummy response.</summary>
    public interface IDummyResultGenerator
    {
        /// <summary>Generate a dummy result when something not right.</summary>
        public Task<InvokeResult> GenerateDummyResultAsync(HttpContext httpContext);

        /// <summary>Generate a dummy result when something not right.</summary>
        public InvokeResult GenerateDummyResult(HttpContext httpContext);
    }
}
