using System;

namespace DentalStudio.Services.Models
{
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    public class DoctorServiceModel : IMapTo<Doctor>, IMapFrom<Doctor>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Specialty { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public string Grade { get; set; }
    }
}
