using IdentityTest.Data;
using IdentityTest.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTest
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            // Create the db and admin user if it doesn't exist
            var scopeFactory = builder.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var adminService = scope.ServiceProvider.GetRequiredService<AdminService>();
                var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                if (db.Database.EnsureCreated())
                {
                    // normally you wouldn't hardcode this password. But for demo purposes, its set here.
                    await adminService.CreateAdmin("Admin123!");
                }
            }
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
