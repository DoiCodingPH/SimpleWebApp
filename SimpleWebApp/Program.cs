using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleWebApp.Data;

namespace SimpleWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var isAdminRoleExist = roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult();
            if (!isAdminRoleExist)
            {
                var adminRole = new IdentityRole("Admin");
                roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            var adminUser = userManager.FindByNameAsync("admin").GetAwaiter().GetResult();
            if (adminUser == null)
            {
                var newAdminUser = new IdentityUser
                {
                    Email = "admin@doicoding.ph",
                    UserName = "admin"
                };
                var result = userManager.CreateAsync(newAdminUser, "MyUnh@ckabl3Pass").GetAwaiter().GetResult();

                var foundUser = userManager.FindByNameAsync("admin").GetAwaiter().GetResult();

                userManager.AddToRoleAsync(foundUser, "Admin");
            }
            else
            {
                var isUserInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!isUserInRole)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

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
