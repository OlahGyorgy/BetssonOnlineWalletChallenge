using RestSharp;

namespace Betsson.OnlineWallets.APITests.Models;

public static class ApiClient
{
    private static readonly string BaseUrl = "http://localhost:8080";
    private static RestClient _client;

    public static RestClient GetClient()
    {
        if (_client == null)
        {
            _client = new RestClient(BaseUrl);
        }
        return _client;
    }
}