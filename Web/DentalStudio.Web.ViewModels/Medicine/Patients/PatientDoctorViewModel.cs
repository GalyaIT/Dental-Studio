namespace DentalStudio.Web.ViewModels.Medicine.Patients
{
    using System.Collections.Generic;
    using System.Globalization;

    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.Infrastructure;
    using DentalStudio.Web.ViewModels.Medicine.Appointments;

    public class PatientDoctorViewModel : IMapFrom<PatientServiceModel>, IHaveCustomMappings
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

        public string IsInsured { get; set; }

        public string IsAlergic { get; set; }

        public IEnumerable<AppointmentDoctorViewModel> Appointments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
             .CreateMap<PatientServiceModel, PatientDoctorViewModel>()
           .ForMember(
                destination => destination.DateOfBirth,
                opts => opts.MapFrom(x => x.DateOfBirth != null ? x.DateOfBirth.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) : "N/A"))
               .ForMember(
                destination => destination.IsAlergic,
                opts => opts.MapFrom(x => BoolExtensions.BoolToString(x.IsAlergic)))
             .ForMember(
                destination => destination.IsInsured,
                opts => opts.MapFrom(x => BoolExtensions.BoolToString(x.IsInsured)))
              .ForMember(
                destination => destination.BloodGroup,
                opts => opts.MapFrom(x => EnumExtensions.GetDisplayName(x.BloodGroup)));
        }
    }
}
