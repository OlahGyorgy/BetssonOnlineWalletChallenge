using RestSharp;
using Serilog;

namespace Betsson.OnlineWallets.APITests.Logging;

public class RestLogger
{
        public static void APILogger(RestRequest request,RestResponse response)
        {
                Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger();
                
                Log.Information("Sending request to  {Url} ", request.Resource);
                Log.Information("Response: {StatusCode}, {ResponseContent}", response.StatusCode, response.Content);
        }
}