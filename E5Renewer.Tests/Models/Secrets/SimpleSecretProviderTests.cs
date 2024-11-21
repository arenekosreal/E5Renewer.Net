using E5Renewer.Models.Secrets;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace E5Renewer.Tests.Models.Secrets;

/// <summary>Test
/// <see cref="SimpleSecretProvider"/>
/// </summary>
[TestClass]
public class SimpleSecretProviderTests
{
    private readonly SimpleSecretProvider provider;

    /// <summary>Initialize <see cref="SimpleSecretProviderTests"/> with no argument.</summary>
    public SimpleSecretProviderTests()
    {
        ILogger<SimpleSecretProvider> logger = Substitute.For<ILogger<SimpleSecretProvider>>();
        FileInfo userSecret = new("test");
        IUserSecretLoader userSecretLoader = Substitute.For<IUserSecretLoader>();
        UserSecret secret = new();
        userSecretLoader.IsSupported(userSecret).Returns(true);
        userSecretLoader.LoadSecretAsync(userSecret).Returns(Task.FromResult(secret));
        IEnumerable<IUserSecretLoader> userSecretLoaders = Substitute.For<IEnumerable<IUserSecretLoader>>();
        IUserSecretLoader[] userSecretLoaderArray = [userSecretLoader];
        userSecretLoaders.GetEnumerator().Returns(userSecretLoaderArray.ToList().GetEnumerator());
        this.provider = new(logger, userSecret, userSecretLoaders);
    }

    /// <summary>Test
    /// <see cref="SimpleSecretProvider.GetPasswordForCertificateAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGetPasswordForCertificateAsync()
    {
        FileInfo noExist = new("no-exist");
        string? password = await this.provider.GetPasswordForCertificateAsync(noExist);
        Assert.IsNull(password);
    }

    /// <summary>Test
    /// <see cref="SimpleSecretProvider.GetRuntimeTokenAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGetRuntimeTokenAsync()
    {
        string token = await this.provider.GetRuntimeTokenAsync();
        bool tokenNull = string.IsNullOrEmpty(token);
        Assert.IsFalse(tokenNull);
    }

    /// <summary>Test
    /// <see cref="SimpleSecretProvider.GetUserSecretAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGetUserSecretAsync()
    {
        UserSecret secret = await this.provider.GetUserSecretAsync();
        Assert.AreEqual(new(), secret);
    }
}
