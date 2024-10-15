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
                
                Log.Information("Request: {Method} {BaseUrl}{Url} ", request.Method,Endpoints.BaseEndpointUrl,request.Resource);
                Log.Information("Response: {StatusCode}, {ResponseContent} \n", response.StatusCode, response.Content);
        }
        
        public static void PostMethodLogger(RestRequest request, object requestBody,RestResponse response)
        {
                Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger();
                
                Log.Information("Request: {Method} {BaseUrl}{Url} \n body: {body}", request.Method,Endpoints.BaseEndpointUrl,request.Resource,requestBody);
                Log.Information("Response: {StatusCode}, {ResponseContent} \n", response.StatusCode, response.Content);
        }
}