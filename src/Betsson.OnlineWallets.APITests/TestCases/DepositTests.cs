using System.Net;
using Betsson.OnlineWallets.APITests.basicSteps;
using NUnit.Framework;

namespace Betsson.OnlineWallets.APITests.TestCases;

public class DepositTests
{
    private PostDepositBalance _depositEndpoint;
    

    [SetUp]
    public async Task Setup()
    {
        _depositEndpoint = new PostDepositBalance();

    }

    
    [Test]
    public async Task PostDepositBalance()
    {
        var response = await _depositEndpoint.PostDepositAsync(100);
        Assert.That(response.StatusCode.Equals( HttpStatusCode.OK));
    }
}