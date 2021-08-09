using DentalStudio.Data.Models;
using DentalStudio.Services.Mapping;
using System;

namespace DentalStudio.Services.Models
{
    public class PostServiceModel : IMapTo<Post>, IMapFrom<Post>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}
