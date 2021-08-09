namespace DentalStudio.Web.ViewModels.Administration.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using DentalStudio.Web.ViewModels.Administration.Procedures;

    public class AppointmentCreateInputModel : IMapTo<AppointmentServiceModel>, IMapFrom<AppointmentServiceModel>
    {
        public AppointmentCreateInputModel()
        {
            this.Doctors = new HashSet<DoctorAppointmentViewModel>();
            this.Patients = new HashSet<PatientAppointmentViewModel>();
            this.Procedures = new HashSet<ProcedureAppointmentViewModel>();
        }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string ProcedureName { get; set; }

        public string Status { get; set; }

        public IEnumerable<PatientAppointmentViewModel> Patients { get; set; }

        public IEnumerable<ProcedureAppointmentViewModel> Procedures { get; set; }

        public IEnumerable<DoctorAppointmentViewModel> Doctors { get; set; }
    }
}
