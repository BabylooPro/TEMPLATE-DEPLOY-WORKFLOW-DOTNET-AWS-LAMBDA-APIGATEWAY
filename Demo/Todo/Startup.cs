using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Todo;

public class Startup
{
    // APPLICATION CONFIGURATION PROPERTY
    public IConfiguration Configuration { get; }

    // CONSTRUCTOR TO INJECT CONFIGURATION
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // CONFIGURE APPLICATION SERVICES - CONTROLLER AND API-ENDPOINT
    public void ConfigureServices(IServiceCollection services)
    {
        // ADD CORS SUPPORT
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

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

        // ENABLE CORS
        app.UseCors();

        app.UseAuthorization();

        // MAP CONTROLLER TO ENDPOINTS
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
