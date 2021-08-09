namespace DentalStudio.Web.ViewModels.Medicine.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Medicine.Patients;
    using DentalStudio.Web.ViewModels.Medicine.Procedures;

    public class DoctorAppointmentCreateModel : IMapTo<AppointmentServiceModel>, IMapFrom<AppointmentServiceModel>
    {
        public DoctorAppointmentCreateModel()
        {
            this.Patients = new HashSet<PatientDoctorAppointmentViewModel>();
            this.Procedures = new HashSet<ProcedureDoctorAppointmentViewModel>();
        }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string ProcedureName { get; set; }

        public string Status { get; set; }

        public IEnumerable<PatientDoctorAppointmentViewModel> Patients { get; set; }

        public IEnumerable<ProcedureDoctorAppointmentViewModel> Procedures { get; set; }
    }
}
