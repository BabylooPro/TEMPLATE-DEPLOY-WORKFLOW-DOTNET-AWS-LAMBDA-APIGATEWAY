using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Todo;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run(); // INITIALIZE AND START THE WEB HOST
    }

    // CREATE AND CONFIGURE THE HOST BUILDER
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
