namespace DentalStudio.Services.Models
{
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using System;

    public class AppointmentServiceModel : IMapTo<Appointment>, IMapFrom<Appointment>
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string DoctorId { get; set; }
        public DoctorServiceModel Doctor { get; set; }

        public string PatientId { get; set; }
        public PatientServiceModel Patient { get; set; }

        public int ProcedureId { get; set; }
        public ProcedureServiceModel Procedure { get; set; }

        public Status Status { get; set; }
    }
}
