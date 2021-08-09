namespace DentalStudio.Web.ViewModels.Patients
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Medicine.Procedures;

    public class PatientAppointmentCreateModel : IMapTo<AppointmentServiceModel>
    {
        public PatientAppointmentCreateModel()
        {
            this.Doctors = new HashSet<DoctorPatientAppointmentViewModel>();
            this.Procedures = new HashSet<ProcedureDoctorAppointmentViewModel>();
        }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        public string ProcedureName { get; set; }

        public string Status { get; set; }

        public IEnumerable<DoctorPatientAppointmentViewModel> Doctors { get; set; }

        public IEnumerable<ProcedureDoctorAppointmentViewModel> Procedures { get; set; }
    }
}
