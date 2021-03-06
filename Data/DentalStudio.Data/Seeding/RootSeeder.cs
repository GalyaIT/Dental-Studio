namespace DentalStudio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal class RootSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var root = new ApplicationUser
            {
                UserName = configuration["Root:UserName"],
                FirstName = configuration["Root:FirstName"],
                LastName = configuration["Root:LastName"],
                Email = configuration["Root:Email"],
            };

            var rootPassword = configuration["Root:Password"];

            var result = await userManager.CreateAsync(root, rootPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(root, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
