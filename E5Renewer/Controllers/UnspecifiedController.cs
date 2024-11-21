using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace E5Renewer.Controllers
{
    /// <summary>All unspecified requests controller.</summary>
    [ApiController]
    public class UnspecifiedController : ControllerBase
    {
        private readonly IDummyResultGenerator dummyResultGenerator;
        private readonly ILogger<UnspecifiedController> logger;

        /// <summary>Initialize <see cref="UnspecifiedController"/> with parameters given.</summary>
        /// <param name="logger">The logger to generate log.</param>
        /// <param name="dummyResultGenerator">The <see cref="IDummyResultGenerator"/> implementation.</param>
        /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
        public UnspecifiedController(ILogger<UnspecifiedController> logger, IDummyResultGenerator dummyResultGenerator) =>
            (this.logger, this.dummyResultGenerator) = (logger, dummyResultGenerator);

        /// <summary>Handle all unspecified requests.</summary>
        [Route("{*method}")]
        public async ValueTask<InvokeResult> Handle()
        {
            this.logger.LogWarning("Someone called a unspecified path {0}.", this.HttpContext.Request.Path);
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return await this.dummyResultGenerator.GenerateDummyResultAsync(this.HttpContext);
        }

    }
}
