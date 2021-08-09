namespace DentalStudio.Web.ViewModels.Posts
{
    using System.Globalization;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class PostViewModel : IMapFrom<PostServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content.Length > 200
                        ? content.Substring(0, 200) + "..."
                        : content;
            }
        }

        public string UserUserName { get; set; }

        public string ImageUrl { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<PostServiceModel, PostViewModel>()
            .ForMember(
               destination => destination.CreatedOn,
               opts => opts.MapFrom(x => x.CreatedOn.ToString("dd MMM yyyy", CultureInfo.InvariantCulture)))
            .ForMember(
               destination => destination.UserUserName,
               opts => opts.MapFrom(x => x.User.UserName));
        }
    }
}
