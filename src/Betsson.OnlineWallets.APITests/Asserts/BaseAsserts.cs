using System.Net;
using NUnit.Framework;
using RestSharp;

namespace Betsson.OnlineWallets.APITests.Asserts;

public class BaseAsserts
{
    
    public static void amountAssert (RestResponse<BalanceResponse> response, double amount)
    {
        statusAssert(response, HttpStatusCode.OK);
        Assert.That(response.Data.Amount, Is.EqualTo(amount));
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