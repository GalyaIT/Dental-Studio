﻿namespace DentalStudio.Web.ViewModels.Administration.Patients
{
    using System.Globalization;

    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.Infrastructure;

    public class PatientDetailsViewModel : IMapTo<PatientServiceModel>, IMapFrom<PatientServiceModel>, IHaveCustomMappings
    {
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
           .CreateMap<PatientServiceModel, PatientDetailsViewModel>()
          .ForMember(
               destination => destination.DateOfBirth,
               opts => opts.MapFrom(x => x.DateOfBirth != null ? x.DateOfBirth.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) : "N/A"))
          .ForMember(
                destination => destination.BloodGroup,
                opts => opts.MapFrom(x => EnumExtensions.GetDisplayName(x.BloodGroup)))
            .ForMember(
                destination => destination.IsAlergic,
                opts => opts.MapFrom(x => BoolExtensions.BoolToString(x.IsAlergic)))
             .ForMember(
                destination => destination.IsInsured,
                opts => opts.MapFrom(x => BoolExtensions.BoolToString(x.IsInsured)));
        }
    }
}
