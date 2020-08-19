using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MSR.Answer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        // https://devblogs.microsoft.com/dotnet/announcing-ef-core-2-0-preview-1/#upgrading-tooling-packages
                        .UseDefaultServiceProvider(options =>
                            options.ValidateScopes = false);
                });
    }
}
