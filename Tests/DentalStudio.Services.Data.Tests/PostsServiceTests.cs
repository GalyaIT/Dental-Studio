namespace DentalStudio.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Data;
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Repositories;
    using DentalStudio.Services.Data.Tests.Common;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Xunit;

    public class PostsServiceTests
    {
        [Fact]
        public async Task Create_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PostsService Create() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);

            var postServiceModel = AutoMapperConfig.MapperInstance.Map<PostServiceModel>(new Post
            {
                ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587376817/blog_photos/iigrfgtym1umxl5ic9u1.jpg",
                Title = "Take care of your smile",
                Content = "Lorem ipsum dolor sit amet, consectetur Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. Aliquam erat volutpat. Sed iaculis lectus ligula, in consequat libero lobortis a. Mauris vulputate orci condimentum ultricies venenatis. In vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra",
                UserId = "1dfrrttyhhhre-kkjhhg-67",
            });

            // Act
            var expectedResult = await postsService.Create(postServiceModel, postServiceModel.UserId);
            var actualResult = 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task Create_WithCorrectData_ShouldSuccessfulyCreate()
        {
            var errorMessagePrefix = "PostsService Create() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);

            var postServiceModel = AutoMapperConfig.MapperInstance.Map<PostServiceModel>(new Post
            {
                ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587376817/blog_photos/iigrfgtym1umxl5ic9u1.jpg",
                Title = "Take care of your smile",
                Content = "Lorem ipsum dolor sit amet, consectetur Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. Aliquam erat volutpat. Sed iaculis lectus ligula, in consequat libero lobortis a. Mauris vulputate orci condimentum ultricies venenatis. In vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra",
                UserId = "1dfrrttyhhhre-kkjhhg-67",
            });

            // Act
            var postsCount = postsRepository.All().Count();
            await postsService.Create(postServiceModel, postServiceModel.UserId);
            var actualResult = postsRepository.All().Count();
            var expectedResult = postsCount + 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Post count is not incremented.");
        }

        [Fact]
        public async Task Edit_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PostsService Edit() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);
            var postServiceModel = postsRepository.All().First().To<PostServiceModel>();
            postServiceModel.Title = "New Title";

            // Act
            var expectedResult = await postsService.Edit(postServiceModel.Id, postServiceModel);
            var actualResult = 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task Delete_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PostsService Delete() method does not work properly.";
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);
            var postServiceModel = postsRepository.All().First().To<PostServiceModel>();

            // Act
            var result = await postsService.Delete(postServiceModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task Delete_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "PostsService Delete() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);
            var postServiceModel = postsRepository.All().First().To<PostServiceModel>();

            // Act
            var postsCount = postsRepository.All().Count();
            await postsService.Delete(postServiceModel);
            var actualResult = postsRepository.All().Count();
            var expectedResult = postsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Posts count is not reduced.");
        }

        [Fact]
        public async Task GetAll_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PostsService GetAll() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);

            // Act
            var actualResult = postsService.GetAll<Post>().ToList();
            var expectedResult = this.GetDummyData().ToList();

            // Assert
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.True(actualResult[i].ImageUrl == expectedResult[i].ImageUrl, errorMessagePrefix + " " + "ImageUrl is not returned properly.");
                Assert.True(actualResult[i].Title == expectedResult[i].Title, errorMessagePrefix + " " + "Title is not returned properly.");
                Assert.True(actualResult[i].Content == expectedResult[i].Content, errorMessagePrefix + " " + "Content is not returned properly.");
                Assert.True(actualResult[i].UserId == expectedResult[i].UserId, errorMessagePrefix + " " + "UserId is not returned properly.");
            }
        }

        [Fact]
        public async Task GetById_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PostService GetById() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);
            var id = 1;

            // Act
            var actualResult = postsService.GetById<PostServiceModel>(id);
            var expectedResult = this.GetDummyData().First().To<PostServiceModel>();

            // Assert
            Assert.True(actualResult.Id == expectedResult.Id, errorMessagePrefix + " " + "PostServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetUrl_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PostService GetUrl() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);
            var id = 1;

            // Act
            var actualResult = postsService.GetUrl(id);
            var expectedResult = this.GetDummyData().First().To<PostServiceModel>().ImageUrl;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "ImageUrl is not returned properly.");
        }

        [Fact]
        public async Task GetCount_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PostsService GetCount() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var postsService = new PostsService(postsRepository);

            // Act
            var actualResult = postsService.GetCount();
            var expectedResult = 2;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix);
        }

        private List<Post> GetDummyData()
        {
            return new List<Post>()
            {
                new Post {Id = 1, ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587376817/blog_photos/iigrfgtym1umxl5ic9u1.jpg",
                Title = "Take care of your smile",
                Content = "Lorem ipsum dolor sit amet, consectetur Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra",
                UserId = "1dfrrttyhhhre-kkjhhg-67", },
                new Post {Id = 2, ImageUrl = "https://res.cloudinary.com/dentalstudio-cloud/image/upload/v1587377492/blog_photos/s7y9grxkra1iladcnjqn.jpg",
                Title = "How to get whiter teeth ",
                Content = "Lorem ipsum dolor sit amet, consectetur Pellentesque sagittis mauris eget porta dignissim. Aliquam posuere efficitur eleifend. vel vestibulum purus. Proin sit amet egestas nisi. Fusce viverra pharetra",
                UserId = "1dfrrttyhhhre-kkjhhg-67", },
            };
        }

        private async Task SeedDataAsync(ApplicationDbContext context)
        {
            context.AddRange(this.GetDummyData());
            await context.SaveChangesAsync();
        }
    }
}
