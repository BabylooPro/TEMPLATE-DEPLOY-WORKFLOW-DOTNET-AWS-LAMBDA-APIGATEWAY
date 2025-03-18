using DotNetEnv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Todo;

public class Program
{
    public static void Main(string[] args)
    {
        // LOAD ENVIRONMENT VARIABLES
        Env.Load();

        // CHECK AND LOG ENVIRONMENT VARIABLES
        CheckAndLogEnvVar("CUSTOM_ENV_VAR");
        CheckAndLogEnvVar("CUSTOM_ENV_VAR_2");

        CreateHostBuilder(args).Build().Run(); // INITIALIZE AND START THE WEB HOST
    }

    // CREATE AND CONFIGURE THE HOST BUILDER
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    // HELPER METHOD TO CHECK AND LOG ENVIRONMENT VARIABLES
    private static void CheckAndLogEnvVar(string envVarName)
    {
        string envVarValue = Environment.GetEnvironmentVariable(envVarName);
        bool isEnvVarSet = bool.TryParse(envVarValue, out bool parsedValue);

        if (isEnvVarSet)
        {
            if (parsedValue)
            {
                Console.WriteLine($"{envVarName} IS SET TO TRUE");
            }
            else
            {
                Console.WriteLine($"{envVarName} IS SET TO FALSE");
            }
        }
        else
        {
            Console.WriteLine($"{envVarName} IS NOT SET");
        }
    }
}
