using System.Diagnostics;
using System.Net;
using Betsson.OnlineWallets.APITests.Asserts;
using Betsson.OnlineWallets.APITests.basicSteps;
using Betsson.OnlineWallets.APITests.TestData;
using NUnit.Framework;

namespace Betsson.OnlineWallets.APITests.TestCases;

public class DepositTests
{
    private PostDepositBalance _depositEndpoint;
    private GetBalance _balanceEndpoint;
    

    [SetUp]
    public async Task Setup()
    {
        _depositEndpoint = new PostDepositBalance();
    }

    
    [Test]
    public async Task PostDepositBalance_OK()
    {
        var stopwatch = Stopwatch.StartNew();
        var response = await _depositEndpoint.PostDepositAsync(TestDataFactory.GetRandomDouble());
        stopwatch.Stop();
        BaseAsserts.statusAssert(response, HttpStatusCode.OK,stopwatch,400);
    }
    
    [Test]
    public async Task PostDepositBalance_OK_ZeroAmount()
    {
        var response = await _depositEndpoint.PostDepositAsync(0);
        BaseAsserts.statusAssert(response, HttpStatusCode.OK);
    }
    
    [Test]
    public async Task PostDepositBalance_ERROR_negativeAmount()
    {

      var response = await _depositEndpoint.PostDepositAsync(-1);
      BaseAsserts.errorAssert(response, HttpStatusCode.BadRequest,"'Amount' must be greater than or equal to '0'");
     
    }
    
    [Test]
    public async Task PostDepositBalance_ERROR_invalidAmount()
    {

        var response = await _depositEndpoint.PostDepositAsync("one");
        BaseAsserts.errorAssert(response, HttpStatusCode.BadRequest,"field is required");
     
    }
}