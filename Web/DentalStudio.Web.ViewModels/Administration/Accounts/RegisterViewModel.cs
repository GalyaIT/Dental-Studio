namespace DentalStudio.Web.ViewModels.Administration.Accounts
{
    using System.ComponentModel.DataAnnotations;

    using DentalStudio.Common;
    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;

    public class RegisterViewModel : IMapTo<ApplicationUser>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Потребителско име")]
        [MaxLength(Constraints.UsernameMaxLength)]
        public string UserName { get; set; }

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

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [StringLength(Constraints.PasswordMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете паролата")]
        [Compare("Password", ErrorMessage = ErrorMessages.CompareErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
