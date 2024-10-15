using System.Net;
using Betsson.OnlineWallets.APITests.Asserts;
using Betsson.OnlineWallets.APITests.basicSteps;
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
        await _depositEndpoint.PostDepositAsync(10.999);
        
        var response = await _withdrawEndpoint.PostWithdrawAsync(10.999);
        BaseAsserts.statusAssert(response, HttpStatusCode.OK);
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