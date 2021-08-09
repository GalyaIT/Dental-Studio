namespace DentalStudio.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using DentalStudio.Data.Common.Models;
    using DentalStudio.Data.Models.Enumerations;

    public class Patient : BaseDeletableModel<string>, IAuditInfo, IDeletableEntity
    {
        public Patient()
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

        public DateTime? DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Photo { get; set; }

        public Gender Gender { get; set; }

        public BloodGroup? BloodGroup { get; set; }

        public string Address { get; set; }

        public bool IsInsured { get; set; }

        public bool IsAlergic { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<Treatment> Treatments { get; set; }
    }
}
