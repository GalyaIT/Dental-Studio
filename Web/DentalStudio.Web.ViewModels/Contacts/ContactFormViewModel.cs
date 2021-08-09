namespace DentalStudio.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using DentalStudio.Common;
    using DentalStudio.Web.Infrastructure;

    public class ContactFormViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Вашите имена")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = ErrorMessages.ValidErrorMessage)]
        [Display(Name = "Email адрес")]
        public string Email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [StringLength(Constraints.TitleMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.TitleMinLength)]
        [Display(Name = "Заглавие на съобщението")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [StringLength(Constraints.ContentMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.ContentMinLength)]
        [Display(Name = "Съдържание на съобщението")]
        public string Content { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
