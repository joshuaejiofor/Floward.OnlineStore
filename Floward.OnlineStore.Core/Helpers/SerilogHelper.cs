using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.IO;

namespace Floward.OnlineStore.Core.Helpers
{
    public class SerilogHelper
    {
        public static void SetupLogging()
        {
            var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("development");

            var logDirectory = Environment.GetEnvironmentVariable("LOG_FILEPATH") ?? Directory.GetCurrentDirectory();
            var logLevel = isDev ? LogEventLevel.Warning : LogEventLevel.Error;

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.FromLogContext()
                .WriteTo.File(new JsonFormatter(), Path.Combine(logDirectory, "Logs", "Application.log"),
                    retainedFileCountLimit: 7, fileSizeLimitBytes: null, rollingInterval: RollingInterval.Day, shared: true,
                    restrictedToMinimumLevel: logLevel)
                .WriteTo.Console(restrictedToMinimumLevel: logLevel, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
