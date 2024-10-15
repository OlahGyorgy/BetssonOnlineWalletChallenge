using System.Net;
using Betsson.OnlineWallets.APITests.basicSteps;
using NUnit.Framework;

namespace Betsson.OnlineWallets.APITests.TestCases;

public class WithdrawTests
{
    private PostWithdrawBalance _withdrawEndpoint;
    private PostDepositBalance _depositEndpoint;
    
    [SetUp]
    public async Task Setup()
    {
        _withdrawEndpoint = new PostWithdrawBalance();
        _depositEndpoint = new PostDepositBalance();
        
        await _depositEndpoint.PostDepositAsync(100);

    }
    
    [Test]
    public async Task PostDepositBalance()
    {
        var response = await _withdrawEndpoint.PostWithdrawAsync(100);
        Assert.That(response.StatusCode.Equals( HttpStatusCode.OK));
    }
}