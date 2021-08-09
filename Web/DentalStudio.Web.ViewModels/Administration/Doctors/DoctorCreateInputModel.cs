namespace DentalStudio.Web.ViewModels.Administration.Doctors
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using DentalStudio.Common;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Microsoft.AspNetCore.Http;

    public class DoctorCreateInputModel : IMapTo<DoctorServiceModel>, IMapFrom<DoctorServiceModel>
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
        [DataType(DataType.PhoneNumber, ErrorMessage = ErrorMessages.ValidErrorMessage)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = ErrorMessages.InValidErrorMessage)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Display(Name = "Дата на раждане")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(Constraints.SpecialtyMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.SpecialtyMinLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Специалност")]
        public string Specialty { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [StringLength(Constraints.AddressMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.AddressMinLength)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile Photo { get; set; }

        [MaxLength(Constraints.GradeMaxLength, ErrorMessage = ErrorMessages.MaxLengthErrorMessage)]
        [Display(Name = "Допълнителна информация")]
        public string Grade { get; set; }
    }
}
