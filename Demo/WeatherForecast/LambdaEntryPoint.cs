using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Microsoft.AspNetCore.Hosting;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))] // SET JSON SERIALIZAER FOR LAMBDA FUNCTION

namespace WeatherForecast;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    // INITIALIZE WEB HOST BUILDER
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Program>(); // USE PROGRAM CONFGURATION
    }
}
