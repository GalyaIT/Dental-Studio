namespace DentalStudio.Data.Models
{
    using System;

    using DentalStudio.Data.Common.Models;
    using DentalStudio.Data.Models.Enumerations;

    public class Appointment : BaseDeletableModel<int>, IAuditInfo, IDeletableEntity
    {
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string PatientId { get; set; }

        public Patient Patient { get; set; }

        public string DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public Status Status { get; set; }

        public int? ProcedureId { get; set; }

        public Procedure Procedure { get; set; }
    }
}
