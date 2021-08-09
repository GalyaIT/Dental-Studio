namespace DentalStudio.Web.ViewModels.Posts
{
    using System.Globalization;

    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Ganss.XSS;

    public class PostDetailsViewModel : IMapFrom<PostServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<PostServiceModel, PostDetailsViewModel>()
            .ForMember(
               destination => destination.CreatedOn,
               opts => opts.MapFrom(x => x.CreatedOn.ToString("dd MMM yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
