using System;
using System.Collections.Generic;
using System.Text;

namespace DentalStudio.Web.ViewModels.Medicine.Doctors
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using System.Globalization;

    public class DoctorViewModel : IMapFrom<DoctorServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string Specialty { get; set; }

        public string Photo { get; set; }

        public string Address { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
             .CreateMap<DoctorServiceModel, DoctorViewModel>()
             .ForMember(
                destination => destination.DateOfBirth,
                opts => opts.MapFrom(x => x.DateOfBirth != null ? x.DateOfBirth.Value.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) : "N/A"));
        }
    }
}
