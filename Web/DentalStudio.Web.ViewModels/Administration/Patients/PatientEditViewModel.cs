namespace DentalStudio.Web.ViewModels.Administration.Patients
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using DentalStudio.Common;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Microsoft.AspNetCore.Http;

    public class PatientEditViewModel : IMapTo<PatientServiceModel>, IMapFrom<PatientServiceModel>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Име")]
        [StringLength(Constraints.FirstNameMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Фамилно име")]
        [StringLength(Constraints.LastNameMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.LastNameMinLength)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = ErrorMessages.ValidErrorMessage)]
        [Display(Name = "Еmail адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = ErrorMessages.InValidErrorMessage)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Дата на раждане")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Display(Name = "Кръвна група")]
        [Required]
        public BloodGroup BloodGroup { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [StringLength(Constraints.AddressMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.AddressMinLength)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Моля отбележете ако има здравна осигуровка")]
        public bool IsInsured { get; set; }

        [Display(Name = "Моля отбележете ако има алергии")]
        public bool IsAlergic { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<PatientServiceModel, PatientEditViewModel>()
              .ForMember(
               destination => destination.Photo,
               opts => opts.Ignore());
        }
    }
}
