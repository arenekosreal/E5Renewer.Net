using System.Net;
using System.Net.Http.Json;

using E5Renewer.Models.Statistics;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace E5Renewer.Tests;

/// <summary> Test
/// <see cref="WebApplicationExtends"/>.
/// </summary>
[TestClass]
public class WebApplicationExtendsTests
{
    private static readonly Uri baseAddress = new("http://localhost:65530/");
    /// <summary>Test
    /// <see cref="WebApplicationExtends.UseAuthTokenAuthentication(Microsoft.AspNetCore.Builder.WebApplication, string)"/>
    /// </summary>
    [TestMethod]
    [DataRow("example-auth-token-invalid", HttpStatusCode.Forbidden)]
    [DataRow("example-auth-token", HttpStatusCode.OK)]
    public async Task TestUseAuthTokenAuthentication(string token, HttpStatusCode target)
    {
        const string validAuthToken = "example-auth-token";

        WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
        webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
        using (WebApplication app = webApplicationBuilder.Build())
        {
            app.UseAuthTokenAuthentication(validAuthToken);
            app.MapGet("/", () => "OK");
            await app.StartAsync();
            HttpClient client = app.GetTestClient();

            client.DefaultRequestHeaders.Add("Authentication", token);
            using (HttpResponseMessage response = await client.GetAsync("/"))
            {
                Assert.AreEqual(target, response.StatusCode);
            }
            await app.StopAsync();
        }
    }
    /// <summary>Test
    /// <see cref="WebApplicationExtends.UseAuthTokenAuthentication(Microsoft.AspNetCore.Builder.WebApplication, string)"/>
    /// </summary>
    [TestMethod]
    public async Task TestUseAuthTokenAuthentication()
    {
        WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
        webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
        using (WebApplication app = webApplicationBuilder.Build())
        {
            app.UseAuthTokenAuthentication("example-auth-token");
            app.MapGet("/", () => "OK");
            await app.StartAsync();
            HttpClient client = app.GetTestClient();

            using (HttpResponseMessage response = await client.GetAsync("/"))
            {
                Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            }
            await app.StopAsync();
        }
    }

    /// <summary>Test
    /// <see cref="WebApplicationExtends.UseHttpMethodChecker(WebApplication, string[])"/>
    /// </summary>
    [TestMethod]
    [DataRow("GET", HttpStatusCode.OK)]
    [DataRow("POST", HttpStatusCode.MethodNotAllowed)]
    public async Task TestUseHttpMethodChecker(string method, HttpStatusCode target)
    {
        const string methodAllowed = "GET";

        WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
        webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
        using (WebApplication app = webApplicationBuilder.Build())
        {
            app.UseHttpMethodChecker(methodAllowed);
            app.MapGet("/", () => "OK");
            await app.StartAsync();
            HttpClient client = app.GetTestClient();

            HttpRequestMessage msg = new(new(method), "/");
            using (HttpResponseMessage response = await client.SendAsync(msg))
            {
                Assert.AreEqual(target, response.StatusCode);
            }
            await app.StopAsync();
        }

    }

    /// <summary>Test
    /// <see cref="WebApplicationExtends.UseUnixTimestampChecker(WebApplication, uint)" />
    /// </summary>
    [TestClass]
    public class TestUseUnixTimestampChecker
    {
        /// <summary>Test
        /// <see cref="WebApplicationExtends.UseUnixTimestampChecker(WebApplication, uint)" />
        /// When no timestamp in get request.
        /// </summary>
        [TestMethod]
        public async Task TestUseUnixTimestampCheckerMissingGet()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
            webApplicationBuilder.Services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            using (WebApplication app = webApplicationBuilder.Build())
            {
                app.UseUnixTimestampChecker();
                app.MapGet("/", () => "OK");
                await app.StartAsync();
                HttpClient client = app.GetTestClient();
                using (HttpResponseMessage response = await client.GetAsync("/"))
                {
                    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
                }
                await app.StopAsync();
            }
        }
        /// <summary>Test
        /// <see cref="WebApplicationExtends.UseUnixTimestampChecker(WebApplication, uint)" />
        /// When no timestamp in post request.
        /// </summary>
        [TestMethod]
        public async Task TestUseUnixTimestampCheckerMissingPost()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
            webApplicationBuilder.Services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            using (WebApplication app = webApplicationBuilder.Build())
            {
                app.UseUnixTimestampChecker();
                app.MapPost("/", () => "OK");
                await app.StartAsync();
                HttpClient client = app.GetTestClient();
                using (HttpResponseMessage response = await client.PostAsync("/", new StringContent("{}")))
                {
                    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
                }
                await app.StopAsync();
            }
        }
        /// <summary>Test
        /// <see cref="WebApplicationExtends.UseUnixTimestampChecker(WebApplication, uint)" />
        /// When invalid timestamp in get request.
        /// </summary>
        [TestMethod]
        public async Task TestUseUnixTimestampCheckerInvalidGet()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseTestServer();
            webApplicationBuilder.Services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            using (WebApplication app = webApplicationBuilder.Build())
            {
                app.UseUnixTimestampChecker();
                app.MapGet("/", () => "OK");
                await app.StartAsync();
                HttpClient client = app.GetTestClient();

                UnixTimestampGenerator timestampGenerator = new();
                long badTimestamp = timestampGenerator.GetUnixTimestamp() - 40 * 1000;
                using (HttpResponseMessage response = await client.GetAsync(string.Format("/?timestamp={0}", badTimestamp.ToString())))
                {
                    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
                }
                await app.StopAsync();
            }
        }
        /// <summary>Test
        /// <see cref="WebApplicationExtends.UseUnixTimestampChecker(WebApplication, uint)" />
        /// When invalid timestamp in post request.
        /// </summary>
        [TestMethod]
        public async Task TestUseUnixTimestampCheckerInvalidPost()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
            webApplicationBuilder.Services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            using (WebApplication app = webApplicationBuilder.Build())
            {
                app.UseUnixTimestampChecker();
                app.MapPost("/", () => "OK");
                await app.StartAsync();
                HttpClient client = app.GetTestClient();

                UnixTimestampGenerator timestampGenerator = new();
                long badTimestamp = timestampGenerator.GetUnixTimestamp() - 40 * 1000;
                Dictionary<string, string> data = new()
                {
                    {"timestamp", badTimestamp.ToString()}
                };
                using (HttpResponseMessage response = await client.PostAsync("/", JsonContent.Create(data)))
                {
                    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
                }
                await app.StopAsync();
            }
        }
        /// <summary>Test
        /// <see cref="WebApplicationExtends.UseUnixTimestampChecker(WebApplication, uint)" />
        /// When timestamp is correct in get request.
        /// </summary>
        [TestMethod]
        public async Task TestUseUnixTimestampCheckerGet()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
            webApplicationBuilder.Services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            using (WebApplication app = webApplicationBuilder.Build())
            {
                app.UseUnixTimestampChecker();
                app.MapGet("/", () => "OK");
                await app.StartAsync();
                HttpClient client = app.GetTestClient();

                UnixTimestampGenerator timestampGenerator = new();
                long timestamp = timestampGenerator.GetUnixTimestamp();
                using (HttpResponseMessage response = await client.GetAsync(string.Format("/?timestamp={0}", timestamp.ToString())))
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
                await app.StopAsync();
            }
        }
        /// <summary>Test
        /// <see cref="WebApplicationExtends.UseUnixTimestampChecker(WebApplication, uint)" />
        /// When timestamp is correct in post request.
        /// </summary>
        [TestMethod]
        public async Task TestUseUnixTimestampCheckerPost()
        {
            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.WebHost.UseTestServer((opt) => opt.BaseAddress = WebApplicationExtendsTests.baseAddress);
            webApplicationBuilder.Services.AddSingleton<IUnixTimestampGenerator, UnixTimestampGenerator>();
            using (WebApplication app = webApplicationBuilder.Build())
            {
                app.UseUnixTimestampChecker();
                app.MapPost("/", () => "OK");
                await app.StartAsync();
                HttpClient client = app.GetTestClient();

                UnixTimestampGenerator timestampGenerator = new();
                long timestamp = timestampGenerator.GetUnixTimestamp();
                Dictionary<string, string> data = new()
                {
                    {"timestamp", timestamp.ToString()}
                };
                using (HttpResponseMessage response = await client.PostAsync("/", JsonContent.Create(data)))
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
                await app.StopAsync();
            }
        }
    }
}
