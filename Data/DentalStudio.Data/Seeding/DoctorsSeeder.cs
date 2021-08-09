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

    internal class DoctorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (dbContext.Doctors.Any())
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = "svetlana",
                FirstName = "Svetlana",
                LastName = "Petrova",
                Email = "svetlana@dental.com",
            };

            var userPassword = "123456";

            var result = await userManager.CreateAsync(user, userPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.DoctorRoleName);
            }

            var doctor = new Doctor
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = "1234567897",
                Address = "Sofia",
                Gender = Gender.Female,
                Grade = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec molestie convallis bibendum. Mauris vitae tristique lorem. Nulla facilisi. Proin lorem lacus, ultrices vitae tempor quis, eleifend congue est. Etiam ultrices ac erat eget ullamcorper. Suspendisse volutpat facilisis velit. Maecenas pellentesque massa elit, ac lobortis ipsum consectetur sed. Sed dignissim massa dui, in cursus metus ultrices ac. Ut eget interdum purus. Fusce accumsan facilisis leo et tincidunt. Etiam lobortis ut nibh vel auctor. Quisque nibh quam, mollis sit amet arcu et, consectetur dapibus ligula. Nam dictum sem a ipsum vehicula, sit amet bibendum sem egestas. Phasellus maximus tristique placerat. Nam libero nulla, imperdiet et orci id, scelerisque suscipit urna. Suspendisse potenti.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec molestie convallis bibendum. Mauris vitae tristique lorem. Nulla facilisi. Proin lorem lacus, ultrices vitae tempor quis, eleifend congue est. Etiam ultrices ac erat eget ullamcorper. Suspendisse volutpat facilisis velit. Maecenas pellentesque massa elit, ac lobortis ipsum consectetur sed. Sed dignissim massa dui, in cursus metus ultrices ac. Ut eget interdum purus. Fusce accumsan facilisis leo et tincidunt. Etiam lobortis ut nibh vel auctor. Quisque nibh quam, mollis sit amet arcu et, consectetur dapibus ligula. Nam dictum sem a ipsum vehicula, sit amet bibendum sem egestas. Phasellus maximus tristique placerat. Nam libero nulla, imperdiet et orci id, scelerisque suscipit urna. Suspendisse potenti.",
                Photo = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587376573/doctors_photos/mhax7pok8zydp44pufjf.jpg",
                Specialty = "Endodontist",
                UserId = user.Id,
            };

            await dbContext.Doctors.AddAsync(doctor);
        }
    }
}
