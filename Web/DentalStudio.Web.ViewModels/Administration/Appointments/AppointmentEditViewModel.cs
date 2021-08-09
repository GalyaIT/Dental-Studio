namespace DentalStudio.Web.ViewModels.Administration.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using DentalStudio.Web.ViewModels.Administration.Procedures;

    public class AppointmentEditViewModel : IMapTo<AppointmentServiceModel>, IMapFrom<AppointmentServiceModel>,IHaveCustomMappings
    {
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

        public Status Status { get; set; }

        public IEnumerable<PatientAppointmentViewModel> Patients { get; set; }

        public IEnumerable<ProcedureAppointmentViewModel> Procedures { get; set; }

        public IEnumerable<DoctorAppointmentViewModel> Doctors { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<AppointmentServiceModel, AppointmentEditViewModel>()
            .ForMember(
               destination => destination.DoctorName,
               opts => opts.MapFrom(x => x.Doctor.FirstName + " " + x.Doctor.LastName))
            .ForMember(
               destination => destination.PatientName,
               opts => opts.MapFrom(x => x.Patient.FirstName + " " + x.Patient.LastName));
        }
    }
}
