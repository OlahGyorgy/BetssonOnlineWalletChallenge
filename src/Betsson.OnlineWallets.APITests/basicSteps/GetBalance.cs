using Betsson.OnlineWallets.APITests.BasicSteps;
using Betsson.OnlineWallets.APITests.Logging;
using Betsson.OnlineWallets.APITests.TestData;
using RestSharp;



namespace Betsson.OnlineWallets.APITests.basicSteps;


public class GetBalance
{
    
    public async Task<RestResponse<BalanceResponse>> GetBalanceAsync()
    {  

        
        RestClient _client = ApiClient.GetClient();
        var request = new RestRequest(Endpoints.BalanceEndpointUrl, Method.Get);
        var response = await _client.ExecuteAsync<BalanceResponse>(request);

        ApiTestLogger.GetMethodLogger(request,response);
        
        return response;
    }

    public async Task<double> GetBalanceAmountAsync()
    {
        var response =  GetBalanceAsync();
        return response.Result.Data.Amount;
    }
    

}