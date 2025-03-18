using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Microsoft.AspNetCore.Hosting;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))] // SET JSON SERIALIZAER FOR LAMBDA FUNCTION

namespace Todo;

// TODO FIX: NULLREFERENCEEXCEPTION IN MARSHALLREQUEST METHOD WHEN HANDLING API GATEWAY REQUESTS WITH "APIGatewayHttpApiV2ProxyFunction"
public class LambdaEntryPoint : APIGatewayProxyFunction
{
    // INITIALIZE WEB HOST BUILDER
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>(); // USE STARTUP CONFGURATION
    }
}
