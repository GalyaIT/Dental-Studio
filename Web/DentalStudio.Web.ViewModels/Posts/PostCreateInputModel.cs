namespace DentalStudio.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using DentalStudio.Common;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Microsoft.AspNetCore.Http;

    public class PostCreateInputModel : IMapFrom<PostServiceModel>, IMapTo<PostServiceModel>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [StringLength(Constraints.TitleMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.TitleMinLength)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Съдържание")]
        public string Content { get; set; }

        [Required]
        public IFormFile ImageUrl { get; set; }
    }
}
