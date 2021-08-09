namespace DentalStudio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Models.Enumerations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class PatientsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (dbContext.Patients.Any())
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = "reni",
                FirstName = "Irena",
                LastName = "Todorova",
                Email = "reni@dental.com",
            };

            var userPassword = "123456";

            var result = await userManager.CreateAsync(user, userPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.PatientRoleName);
            }

            var patient = new Patient
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = "2233445566",
                Address = "Plovdiv",
                Gender = Gender.Female,
                Photo = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587376722/patients_photos/z7icbiceyex8kciyzgbj.jpg",
                BloodGroup = BloodGroup.APositive,
                DateOfBirth = DateTime.ParseExact("15/06/1987", "dd/MM/yyyy", null),
                IsAlergic = false,
                IsInsured = true,
                UserId = user.Id,
            };

            await dbContext.Patients.AddAsync(patient);
        }
    }
}
