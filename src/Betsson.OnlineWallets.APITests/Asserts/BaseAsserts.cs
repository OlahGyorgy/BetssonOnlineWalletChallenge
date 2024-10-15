using System.Diagnostics;
using System.Net;
using NUnit.Framework;
using RestSharp;

namespace Betsson.OnlineWallets.APITests.Asserts;

public class BaseAsserts
{
    public static void amountAssert (RestResponse<BalanceResponse> response, double expectedAmount)
    {
        statusAssert(response, HttpStatusCode.OK);
        var actualAmount = response.Data.Amount;
        Assert.That(actualAmount, Is.EqualTo(expectedAmount).Within(0.00001),$"The actual amount: {actualAmount} is not what expected {expectedAmount}");
    }
    public static void statusAssert(RestResponse<BalanceResponse> response, HttpStatusCode expectedStatusCode)
    {
        Assert.That(response.StatusCode.Equals( expectedStatusCode), "Expected status code does not match");
    }
    
    public static void statusAssert(RestResponse<BalanceResponse> response, HttpStatusCode expectedStatusCode, Stopwatch stopwatch, double expectedResponseTimeMilliseconds)
    {
        TimeSpan expectedResponseTime = TimeSpan.FromMilliseconds(expectedResponseTimeMilliseconds);
        TimeSpan actualResponseTime = stopwatch.Elapsed;
    
        Assert.That(actualResponseTime, Is.LessThanOrEqualTo(expectedResponseTime), 
            $"Expected response time to be less than {expectedResponseTime.TotalMilliseconds}ms, but got {actualResponseTime.TotalMilliseconds}ms.");

        Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode), 
            $"Expected status code {expectedStatusCode} but got {response.StatusCode}.");
    }
    
    public static void errorAssert(RestResponse<BalanceResponse> response, HttpStatusCode expectedStatusCode, String errorMessage)
    {
        Assert.That(response.StatusCode.Equals( expectedStatusCode), "Expected status code does not match");
        Assert.That(response.Content.Contains(errorMessage),"Should contains this error message: "+errorMessage);

    }
}