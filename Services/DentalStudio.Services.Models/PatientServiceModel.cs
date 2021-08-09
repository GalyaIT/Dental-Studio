namespace DentalStudio.Services.Models
{
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using System;

    public class PatientServiceModel : IMapTo<Patient>, IMapFrom<Patient>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age => (int)((DateTime.Now - this.DateOfBirth).TotalDays / 365.242199);

        public string Photo { get; set; }

        public Gender Gender { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public string Address { get; set; }

        public bool IsInsured { get; set; }

        public bool IsAlergic { get; set; }
    }
}
