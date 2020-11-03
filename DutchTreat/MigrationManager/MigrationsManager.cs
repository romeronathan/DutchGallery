using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat.MigrationManager
{
    public static class MigrationsManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            {

                using (var scope = host.Services.CreateScope())
                {
                    using (var dutchContext = scope.ServiceProvider.GetRequiredService<DutchSeeder>())
                    {
                        try
                        {
                            dutchContext.SeedAsync().Wait();
                        }
                        catch
                        {
                            throw;
                        }


                    }
                }
                return host;
            }
        }
    }
}
