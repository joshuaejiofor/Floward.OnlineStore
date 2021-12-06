using Floward.OnlineStore.Core.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;
using System.Reflection;

namespace Floward.OnlineStore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogHelper.SetupLogging();
            Log.Warning("Starting the web api host..");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())                
                .UseSerilog();
    }
}
