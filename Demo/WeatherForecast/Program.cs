using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecast.Middleware;

namespace WeatherForecast;

public class Program
{
    public static void Main(string[] args)
    {
        // LOAD ENVIRONMENT VARIABLES
        Env.Load();

        // CHECK AND LOG ENVIRONMENT VARIABLE
        CheckAndLogEnvVar("CUSTOM_ENV_VAR");
        CheckAndLogEnvVar("CUSTOM_ENV_VAR_2");

        CreateHostBuilder(args).Build().Run(); // INITIALZE AND START THE WEB HOST
    }

    // CREATE AND CONFIGURE THE HOST BUILDER
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Program>();
            });

    // APPLICATION CONFIGURATION PROPERTY
    public IConfiguration Configuration { get; }

    // CONSTRUCTOR TO INJECT CONFIGURATION
    public Program(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // CONFIURE APPLICATION SERVICES - CONTROLLER AND API-ENDPOINT
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // CONFIGURE APPLICATION MIDDLEWARE
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ENABLE SWAGGER (ONLY IN DEV MODE)
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // ENV LOGGING MIDDLEWARE - WILL LOG ENVIRONMENT VARIABLES FOR EACH REQUEST
        app.UseEnvLogging();

        // GLOBAL MIDDLEWARE CONFIGURATION
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        // MAP CONTROLLER TO ENDPOINTS
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    // PUBLIC METHOD TO LOG ENVIRONMENT VARIABLES - CALLED BY MIDDLEWARE
    public static void LogEnvironmentVariables()
    {
        CheckAndLogEnvVar("CUSTOM_ENV_VAR");
        CheckAndLogEnvVar("CUSTOM_ENV_VAR_2");
    }

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
