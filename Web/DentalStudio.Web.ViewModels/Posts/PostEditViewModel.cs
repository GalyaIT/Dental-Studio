namespace DentalStudio.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using DentalStudio.Common;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Microsoft.AspNetCore.Http;

    public class PostEditViewModel : IMapFrom<PostServiceModel>, IMapTo<PostServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [StringLength(Constraints.TitleMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.TitleMinLength)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Съдържание")]
        public string Content { get; set; }

        public IFormFile ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
             .CreateMap<PostServiceModel, PostEditViewModel>()
               .ForMember(
                destination => destination.ImageUrl,
                opts => opts.Ignore());
        }
    }
}
