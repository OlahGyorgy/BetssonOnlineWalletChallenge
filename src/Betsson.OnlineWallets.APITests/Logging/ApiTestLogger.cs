using Betsson.OnlineWallets.APITests.TestData;
using RestSharp;
using Serilog;

namespace Betsson.OnlineWallets.APITests.Logging;

public class ApiTestLogger
{
        public static void GetMethodLogger(RestRequest request,RestResponse response)
        {
                Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger();
                
                Log.Information("Sending request to {BaseUrl}{Url} ", Endpoints.BaseEndpointUrl,request.Resource);
                Log.Information("Response: {StatusCode}, {ResponseContent}", response.StatusCode, response.Content);
        }
        
        public static void PostMethodLogger(RestRequest request, object requestBody,RestResponse response)
        {
                Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger();
                
                Log.Information("Sending request to {BaseUrl}{Url} \n body: {body}", Endpoints.BaseEndpointUrl,request.Resource,requestBody);
                Log.Information("Response: {StatusCode}, {ResponseContent}", response.StatusCode, response.Content);
        }
}