namespace DentalStudio.Data.Models
{
    using System;
    using System.Collections.Generic;

    using DentalStudio.Data.Common.Models;

    public class Treatment : BaseDeletableModel<int>, IAuditInfo, IDeletableEntity
    {
        public Treatment()
        {
            this.Medecines = new HashSet<Medecine>();
        }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PatientId { get; set; }

        public Patient Patient { get; set; }

        public string DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public ICollection<Medecine> Medecines { get; set; }
    }
}
