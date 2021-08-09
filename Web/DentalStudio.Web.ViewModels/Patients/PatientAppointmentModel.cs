﻿namespace DentalStudio.Web.ViewModels.Patients
{
    using System.Globalization;

    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class PatientAppointmentModel : IMapFrom<AppointmentServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

        public string Status { get; set; }

        public string ProcedureName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
             .CreateMap<AppointmentServiceModel, PatientAppointmentModel>()
             .ForMember(
                destination => destination.Date,
                opts => opts.MapFrom(x => x.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)))
             .ForMember(
                destination => destination.Time,
                opts => opts.MapFrom(x => x.Time.ToString("HH:mm", CultureInfo.InvariantCulture)))
             .ForMember(
                destination => destination.DoctorName,
                opts => opts.MapFrom(x => x.Doctor.FirstName + " " + x.Doctor.LastName))
             .ForMember(
                destination => destination.PatientName,
                opts => opts.MapFrom(x => x.Patient.FirstName + " " + x.Patient.LastName));
        }
    }
}
