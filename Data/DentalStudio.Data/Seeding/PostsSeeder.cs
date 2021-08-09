namespace DentalStudio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal class PostsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            if (dbContext.Posts.Any())
            {
                return;
            }

            var admin = dbContext.Users.FirstOrDefault(u => u.UserName == configuration["Root:UserName"]);

            await dbContext.Posts.AddAsync(
                new Models.Post
                {
                    ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587376817/blog_photos/iigrfgtym1umxl5ic9u1.jpg",
                    Title = "Take care of your smile",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec libero a nisl pellentesque rutrum. Aenean tincidunt finibus felis, sed porta diam vehicula in. Ut ante magna, consectetur ullamcorper leo nec, volutpat mollis magna. Nunc tristique finibus augue. Morbi non sem ullamcorper, semper elit ac, imperdiet magna. Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. Aliquam erat volutpat. Sed iaculis lectus ligula, in consequat libero lobortis a. Mauris vulputate orci condimentum ultricies venenatis. In vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra enim, a blandit neque egestas in. Curabitur non erat a lacus posuere laoreet. Vivamus pharetra lacus eget velit lobortis auctor. Morbi mi ante, pretium at nibh vitae, feugiat cursus turpis",
                    UserId = admin.Id,
                });
            await dbContext.Posts.AddAsync(
                 new Models.Post
                 {
                     ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587377492/blog_photos/s7y9grxkra1iladcnjqn.jpg",
                     Title = "How to get whiter teeth ",
                     Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec libero a nisl pellentesque rutrum. Aenean tincidunt finibus felis, sed porta diam vehicula in. Ut ante magna, consectetur ullamcorper leo nec, volutpat mollis magna. Nunc tristique finibus augue. Morbi non sem ullamcorper, semper elit ac, imperdiet magna. Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. Aliquam erat volutpat. Sed iaculis lectus ligula, in consequat libero lobortis a. Mauris vulputate orci condimentum ultricies venenatis. In vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra enim, a blandit neque egestas in. Curabitur non erat a lacus posuere laoreet. Vivamus pharetra lacus eget velit lobortis auctor. Morbi mi ante, pretium at nibh vitae, feugiat cursus turpis",
                     UserId = admin.Id,
                 });
            await dbContext.Posts.AddAsync(
               new Models.Post
               {
                   ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587377474/blog_photos/wn24pfacqps6lhkxf1op.jpg",
                   Title = "10 Tips for beter teeth",
                   Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec libero a nisl pellentesque rutrum. Aenean tincidunt finibus felis, sed porta diam vehicula in. Ut ante magna, consectetur ullamcorper leo nec, volutpat mollis magna. Nunc tristique finibus augue. Morbi non sem ullamcorper, semper elit ac, imperdiet magna. Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. Aliquam erat volutpat. Sed iaculis lectus ligula, in consequat libero lobortis a. Mauris vulputate orci condimentum ultricies venenatis. In vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra enim, a blandit neque egestas in. Curabitur non erat a lacus posuere laoreet. Vivamus pharetra lacus eget velit lobortis auctor. Morbi mi ante, pretium at nibh vitae, feugiat cursus turpis",
                   UserId = admin.Id,
               });
            await dbContext.Posts.AddAsync(
               new Models.Post
               {
                   ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587377518/blog_photos/qdmorshdcfc0syrhgdhc.jpg",
                   Title = "How to get whiter teeth in 2 weeks",
                   Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec libero a nisl pellentesque rutrum. Aenean tincidunt finibus felis, sed porta diam vehicula in. Ut ante magna, consectetur ullamcorper leo nec, volutpat mollis magna. Nunc tristique finibus augue. Morbi non sem ullamcorper, semper elit ac, imperdiet magna. Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. Aliquam erat volutpat. Sed iaculis lectus ligula, in consequat libero lobortis a. Mauris vulputate orci condimentum ultricies venenatis. In vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra enim, a blandit neque egestas in. Curabitur non erat a lacus posuere laoreet. Vivamus pharetra lacus eget velit lobortis auctor. Morbi mi ante, pretium at nibh vitae, feugiat cursus turpis",
                   UserId = admin.Id,
               });
            await dbContext.Posts.AddAsync(
               new Models.Post
               {
                   ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587377492/blog_photos/s7y9grxkra1iladcnjqn.jpg",
                   Title = "Stained Teeth",
                   Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec libero a nisl pellentesque rutrum. Aenean tincidunt finibus felis, sed porta diam vehicula in. Ut ante magna, consectetur ullamcorper leo nec, volutpat mollis magna. Nunc tristique finibus augue. Morbi non sem ullamcorper, semper elit ac, imperdiet magna. Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. Aliquam erat volutpat. Sed iaculis lectus ligula, in consequat libero lobortis a. Mauris vulputate orci condimentum ultricies venenatis. In vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra enim, a blandit neque egestas in. Curabitur non erat a lacus posuere laoreet. Vivamus pharetra lacus eget velit lobortis auctor. Morbi mi ante, pretium at nibh vitae, feugiat cursus turpis",
                   UserId = admin.Id,
               });
        }
    }
}
