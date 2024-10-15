using System.Net;
using Betsson.OnlineWallets.APITests.Asserts;
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
    public async Task GetBalance_OK()
    {
        var response = await _balanceEndpoint.GetBalanceAsync();
        BaseAsserts.statusAssert(response, HttpStatusCode.OK);
    }
}

