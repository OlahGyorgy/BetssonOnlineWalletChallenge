using System.Net;
using Betsson.OnlineWallets.APITests.basicSteps;
using NUnit.Framework;

namespace Betsson.OnlineWallets.APITests.TestCases;

[TestFixture]
public class BalanceTests
{
    private GetBalance _balanceEndpoint;
    

    [SetUp]
    public async Task Setup()
    {
        _balanceEndpoint = new GetBalance();

    }

    
    [Test]
    public async Task GetBalance()
    {
        var response = await _balanceEndpoint.GetBalanceAsync();
        Assert.That(response.StatusCode.Equals( HttpStatusCode.OK));
    }
}

