using System.Net;
using Betsson.OnlineWallets.APITests.Asserts;
using Betsson.OnlineWallets.APITests.basicSteps;
using Betsson.OnlineWallets.APITests.TestData;
using NUnit.Framework;

namespace Betsson.OnlineWallets.APITests.TestCases;

public class FunctionalTests
{
    private PostWithdrawBalance _withdrawEndpoint;
    private PostDepositBalance _depositEndpoint;
    private GetBalance _balanceEndpoint;
    private double _initialBalance;
    
    [SetUp]
    public async Task Setup()
    {
        _withdrawEndpoint = new PostWithdrawBalance();
        _depositEndpoint = new PostDepositBalance();
        _balanceEndpoint = new GetBalance();
        _initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();

    }
    
    [Test]
    public async Task OK_DepositsThenWithdraw()
    {
        double depositAmount = TestDataFactory.GetRandomDouble();
        double expectedBalance = _initialBalance + depositAmount;
        
        
        var response1 = await _depositEndpoint.PostDepositAsync(depositAmount);
        BaseAsserts.amountAssert(response1, expectedBalance);
        
        var response2 = await _balanceEndpoint.GetBalanceAsync();
        BaseAsserts.amountAssert(response2, expectedBalance);
        expectedBalance = response2.Data.Amount + depositAmount;
        
        var response3 = await _depositEndpoint.PostDepositAsync(depositAmount);
        BaseAsserts.amountAssert(response3, expectedBalance);
        
        var response4 = await _balanceEndpoint.GetBalanceAsync();
        BaseAsserts.amountAssert(response4, expectedBalance);
        expectedBalance = 0 ;
        
        var response5 = await _withdrawEndpoint.PostWithdrawAsync(response4.Data.Amount);
        BaseAsserts.amountAssert(response5, expectedBalance);
        
        var response6 = await _balanceEndpoint.GetBalanceAsync();
        BaseAsserts.amountAssert(response6, expectedBalance);
       
    }
    
    [Test]
    public async Task ERROR_InsufficientBalance()
    {

        double creditAmount = TestDataFactory.GetRandomDouble();
        double withdrawAmount = _initialBalance + creditAmount;
        
        
        var response1 = await _withdrawEndpoint.PostWithdrawAsync(withdrawAmount);
        BaseAsserts.errorAssert(response1, HttpStatusCode.BadRequest,"InsufficientBalanceException");
        

    }
}