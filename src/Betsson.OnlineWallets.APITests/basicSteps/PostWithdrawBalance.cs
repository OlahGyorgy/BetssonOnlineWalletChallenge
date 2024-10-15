using Betsson.OnlineWallets.APITests.BasicSteps;
using Betsson.OnlineWallets.APITests.Logging;
using Betsson.OnlineWallets.APITests.TestData;
using RestSharp;

namespace Betsson.OnlineWallets.APITests.basicSteps;

public class PostWithdrawBalance
{
    public async Task<RestResponse<BalanceResponse>> PostWithdrawAsync(double amount)
    {  

        
        RestClient _client = ApiClient.GetClient();
        var request = new RestRequest(Endpoints.WithdrawEndpointUrl, Method.Post);
        var requestBody = new { amount };
        request.AddJsonBody(requestBody);
        var response = await _client.ExecuteAsync<BalanceResponse>(request);
        
        ApiTestLogger.PostMethodLogger(request,requestBody,response);
        
        return response;
    }
    
    public async Task<RestResponse<BalanceResponse>> PostWithdrawAsync(String amount)
    {  

        
        RestClient _client = ApiClient.GetClient();
        var request = new RestRequest(Endpoints.WithdrawEndpointUrl, Method.Post);
        var requestBody = new { amount };
        request.AddJsonBody(requestBody);
        var response = await _client.ExecuteAsync<BalanceResponse>(request);
        
        ApiTestLogger.PostMethodLogger(request,requestBody,response);
        
        return response;
    }
}