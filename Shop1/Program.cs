using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shop1.Data;
using Shop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                service.Database.Migrate();

                service.Database.ExecuteSqlRaw(@"IF NOT EXISTS (select 1 from Users) BEGIN INSERT INTO Users(Email, Password, Role) VALUES('admin@admin.ba', '12345678', 'admin') END IF NOT EXISTS (SELECT 1 FROM Products) BEGIN INSERT INTO Products(Name,Price,Description,Image) VALUES ('Shoei RF-SR Helmet - Solid',1,'Full Face Helmet','/1024x768-f2b21802-64bc-11eb-a115-0242ac130010.webp') INSERT INTO Products(Name,Price,Description,Image) VALUES ('AGV K1 Helmet',2,'Full Face Helmet','agvk1_helmet_750x750.jpg	')  END");
            };
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
