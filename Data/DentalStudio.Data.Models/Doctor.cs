namespace DentalStudio.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using DentalStudio.Data.Common.Models;
    using DentalStudio.Data.Models.Enumerations;

    public class Doctor : BaseDeletableModel<string>, IAuditInfo, IDeletableEntity
    {
        public Doctor()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Appointments = new HashSet<Appointment>();
            this.Treatments = new HashSet<Treatment>();
            this.CreatedOn = DateTime.UtcNow;
        }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

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

        public ICollection<Appointment> Appointments { get; private set; }

        public ICollection<Treatment> Treatments { get; set; }
    }
}
