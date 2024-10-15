using System.Diagnostics;
using System.Net;
using Betsson.OnlineWallets.APITests.Asserts;
using Betsson.OnlineWallets.APITests.basicSteps;
using Betsson.OnlineWallets.APITests.TestData;
using NUnit.Framework;

namespace Betsson.OnlineWallets.APITests.TestCases;

public class WithdrawTests
{
    private PostWithdrawBalance _withdrawEndpoint;
    private PostDepositBalance _depositEndpoint;
    private GetBalance _balanceEndpoint;
    
    [SetUp]
    public async Task Setup()
    {
        _withdrawEndpoint = new PostWithdrawBalance();
        _depositEndpoint = new PostDepositBalance();


    }
    
    [Test]
    public async Task PostWithdrawBalance_OK()
    {
        double randomValue = TestDataFactory.GetRandomDouble();
        await _depositEndpoint.PostDepositAsync(randomValue);
        
        var stopwatch = Stopwatch.StartNew();
        var response = await _withdrawEndpoint.PostWithdrawAsync(randomValue);
        stopwatch.Stop();
        BaseAsserts.statusAssert(response, HttpStatusCode.OK,stopwatch,400);
    }
    

    
    [Test]
    public async Task PostWithdrawBalance_ERROR_zeroWithdraw()
    {
        var response = await _withdrawEndpoint.PostWithdrawAsync(0);
        BaseAsserts.statusAssert(response, HttpStatusCode.OK);
    }
    
    [Test]
    public async Task PostWithdrawBalance_ERROR_negativeAmount()
    {

        var response = await _withdrawEndpoint.PostWithdrawAsync(-1);
        BaseAsserts.errorAssert(response, HttpStatusCode.BadRequest,"'Amount' must be greater than or equal to '0'");
     
    }
    
    [Test]
    public async Task PostWithdrawBalance_ERROR_invalidAmount()
    {

        var response = await _withdrawEndpoint.PostWithdrawAsync("one tousand");
        BaseAsserts.errorAssert(response, HttpStatusCode.BadRequest,"field is required");
     
    }
}