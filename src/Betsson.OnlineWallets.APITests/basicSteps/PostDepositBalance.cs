﻿using Betsson.OnlineWallets.APITests.BasicSteps;
using Betsson.OnlineWallets.APITests.Logging;
using Betsson.OnlineWallets.APITests.TestData;
using RestSharp;

namespace Betsson.OnlineWallets.APITests.basicSteps;

public class PostDepositBalance
{
    public async Task<RestResponse<BalanceResponse>> PostDepositAsync(double amount)
    {  

        
        RestClient _client = ApiClient.GetClient();
        var request = new RestRequest(Endpoints.DepositEndpointUrl, Method.Post);
        var requestBody = new { amount };
        request.AddJsonBody(requestBody);
        var response = await _client.ExecuteAsync<BalanceResponse>(request);
        
        ApiTestLogger.PostMethodLogger(request,requestBody,response);
        
        return response;
    }
    
    public async Task<RestResponse<BalanceResponse>> PostDepositAsync(String amount)
    {  

        
        RestClient _client = ApiClient.GetClient();
        var request = new RestRequest(Endpoints.DepositEndpointUrl, Method.Post);
        var requestBody = new { amount };
        Console.WriteLine(requestBody.ToString());
        request.AddJsonBody(requestBody);
        var response = await _client.ExecuteAsync<BalanceResponse>(request);
        
        ApiTestLogger.PostMethodLogger(request,requestBody,response);
        
        return response;
    }
}