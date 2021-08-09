namespace DentalStudio.Web.ViewModels.Administration.Appointments
{
    using System.Globalization;

    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class AppointmentDetailsViewModel : IMapTo<AppointmentServiceModel>, IMapFrom<AppointmentServiceModel>, IHaveCustomMappings
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

        public string ProcedureName { get; set; }

        public string Status { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<AppointmentServiceModel, AppointmentDetailsViewModel>()
             .ForMember(
                destination => destination.Date,
                opts => opts.MapFrom(x => x.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)))
             .ForMember(
                destination => destination.Time,
                opts => opts.MapFrom(x => x.Time.ToString("HH:mm")))
            .ForMember(
               destination => destination.DoctorName,
               opts => opts.MapFrom(x => x.Doctor.FirstName + " " + x.Doctor.LastName))
            .ForMember(
               destination => destination.PatientName,
               opts => opts.MapFrom(x => x.Patient.FirstName + " " + x.Patient.LastName));
        }
    }
}
