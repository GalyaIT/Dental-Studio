namespace DentalStudio.Web.ViewModels.Patients
{
    using System.Globalization;

    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Ganss.XSS;

    public class PatientDoctorDetailsViewModel : IMapTo<DoctorServiceModel>, IMapFrom<DoctorServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string Specialty { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public string Grade { get; set; }

        public string SanitizedGrade => new HtmlSanitizer().Sanitize(this.Grade);

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<DoctorServiceModel, PatientDoctorDetailsViewModel>()
            .ForMember(
               destination => destination.DateOfBirth,
               opts => opts.MapFrom(x => x.DateOfBirth != null ? x.DateOfBirth.Value.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) : "N/A"));
        }
    }
}
