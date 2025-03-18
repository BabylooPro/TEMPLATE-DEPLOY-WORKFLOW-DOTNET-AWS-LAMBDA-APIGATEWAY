using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WeatherForecast;

public class Program
{
    public static void Main(string[] args)
    {
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
}
