using System.Diagnostics;
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
        var stopwatch = Stopwatch.StartNew();
        var response = await _balanceEndpoint.GetBalanceAsync();
        stopwatch.Stop();
        BaseAsserts.statusAssert(response, HttpStatusCode.OK,stopwatch,400);
        
    }
}

