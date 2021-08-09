namespace DentalStudio.Web.ViewModels.Administration.Patients
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using System.Globalization;

    public class PatientViewModel : IMapFrom<PatientServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Photo { get; set; }

        public string Gender { get; set; }

        public string BloodGroup { get; set; }

        public string Address { get; set; }

        public bool IsInsured { get; set; }

        public bool IsAlergic { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
             .CreateMap<PatientServiceModel, PatientViewModel>()
           .ForMember(
                destination => destination.DateOfBirth,
                opts => opts.MapFrom(x => x.DateOfBirth != null ? x.DateOfBirth.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) : "N/A"));
        }
    }
}
