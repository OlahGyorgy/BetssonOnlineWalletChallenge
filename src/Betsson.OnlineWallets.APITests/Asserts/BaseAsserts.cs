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
        Assert.That(actualAmount, Is.EqualTo(expectedAmount).Within(0.000001),$"The actual amount: {actualAmount} is not what expected {expectedAmount}");
    }
    public static void statusAssert(RestResponse<BalanceResponse> response, HttpStatusCode expectedStatusCode)
    {
        Assert.That(response.StatusCode.Equals( expectedStatusCode), "Expected status code does not match");

    }
    
    public static void errorAssert(RestResponse<BalanceResponse> response, HttpStatusCode expectedStatusCode, String errorMessage)
    {
        Assert.That(response.StatusCode.Equals( expectedStatusCode), "Expected status code does not match");
        Assert.That(response.Content.Contains(errorMessage),"Should contains this error message: "+errorMessage);

    }
}