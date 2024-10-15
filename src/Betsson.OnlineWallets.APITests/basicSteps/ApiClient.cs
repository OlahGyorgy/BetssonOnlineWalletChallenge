using Betsson.OnlineWallets.APITests.TestData;
using RestSharp;

namespace Betsson.OnlineWallets.APITests.BasicSteps;

public static class ApiClient
{
    private static RestClient _client;

    public static RestClient GetClient()
    {
        if (_client == null)
        {
            _client = new RestClient(Endpoints.BaseEndpointUrl);
        }
        return _client;
    }
}