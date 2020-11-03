using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DutchTreat.MigrationManager;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)

        {

            CreateHostBuilder(args).Build().MigrateDatabase().Run();


            
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetUpConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                   
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetUpConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Removing the defaul configuration options
            builder.Sources.Clear();
            builder.AddJsonFile("config.json", false, true)
                .AddEnvironmentVariables();

        }
    }

  
}
