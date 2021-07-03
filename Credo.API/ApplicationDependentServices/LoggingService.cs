using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace Credo.API.ApplicationDependentServices
{
    public static class LoggingService
    {
        public static void CreateLoggingService()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
        }
    }
}