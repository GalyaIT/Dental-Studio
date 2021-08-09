namespace DentalStudio.Web.ViewModels.Medicine.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Medicine.Patients;
    using DentalStudio.Web.ViewModels.Medicine.Procedures;

    public class DoctorAppointmentEditViewModel : IMapTo<AppointmentServiceModel>, IMapFrom<AppointmentServiceModel>, IHaveCustomMappings
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]

        public string PatientName { get; set; }

        [Required]

        public string ProcedureName { get; set; }

        public Status Status { get; set; }

        public IEnumerable<PatientDoctorAppointmentViewModel> Patients { get; set; }

        public IEnumerable<ProcedureDoctorAppointmentViewModel> Procedures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<AppointmentServiceModel, DoctorAppointmentEditViewModel>()
            .ForMember(
               destination => destination.PatientName,
               opts => opts.MapFrom(x => x.Patient.FirstName + " " + x.Patient.LastName));
        }
    }
}
