using Betsson.OnlineWallets.APITests.Logging;
using Betsson.OnlineWallets.APITests.Models;
using RestSharp;



namespace Betsson.OnlineWallets.APITests;


public class GetBalance
{
    
    public async Task<RestResponse<BalanceResponse>> GetBalanceAsync()
    {  

        
        RestClient _client = ApiClient.GetClient();
        var request = new RestRequest("/onlinewallet/balance", Method.Get);
        var response = await _client.ExecuteAsync<BalanceResponse>(request);

        RestLogger.APILogger(request,response);
        
        return response;
    }
    

}