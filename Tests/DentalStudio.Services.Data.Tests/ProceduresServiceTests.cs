namespace DentalStudio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Data;
    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Data.Repositories;
    using DentalStudio.Services.Data.Tests.Common;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Moq;
    using Xunit;

    public class ProceduresServiceTests
    {
        [Fact]
        public async Task Create_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ProceduresService Create() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);

            var procedureServiceModel = AutoMapperConfig.MapperInstance.Map<ProcedureServiceModel>(new Procedure { Name = "Лечение на пулпит или периодонтит на постоянен зъб", Code = Code.ThreeHundredThirtyThree, Price = 80 });

            // Act
            var result = await proceduresService.Create(procedureServiceModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task Create_WithCorrectData_ShouldSuccessfulyCreate()
        {
            var errorMessagePrefix = "ProcedureService Create() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);

            var procedureServiceModel = AutoMapperConfig.MapperInstance.Map<ProcedureServiceModel>(new Procedure { Name = "Лечение на пулпит или периодонтит на постоянен зъб", Code = Code.ThreeHundredThirtyThree, Price = 80 });

            // Act
            var proceduresCount = proceduresRepository.All().Count();
            await proceduresService.Create(procedureServiceModel);
            var actualResult = proceduresRepository.All().Count();
            var expectedResult = proceduresCount + 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Procedures count is not incremented.");
        }

        [Fact]
        public async Task Edit_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ProceduresService Edit() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);
            var procedureServiceModel = proceduresRepository.All().First().To<ProcedureServiceModel>();
            procedureServiceModel.Price = decimal.Parse("100");

            // Act
            var result = await proceduresService.Edit(procedureServiceModel.Id, procedureServiceModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task Delete_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ProceduresService Delete() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);
            var procedureServiceModel = proceduresRepository.All().First().To<ProcedureServiceModel>();

            // Act
            var result = await proceduresService.Delete(procedureServiceModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task Delete_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "ProceduresService Delete() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);
            var procedureServiceModel = proceduresRepository.All().First().To<ProcedureServiceModel>();

            // Act
            var proceduresCount = proceduresRepository.All().Count();
            await proceduresService.Delete(procedureServiceModel);
            var actualResult = proceduresRepository.All().Count();
            var expectedResult = proceduresCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Procedures count is not reduced.");
        }

        [Fact]
        public async Task GetAll_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ProcedureService GetAll() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);

            // Act
            var actualResult = proceduresService.GetAll<Procedure>().ToList();
            var expectedResult = this.GetDummyData().ToList();

            // Assert
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.True(actualResult[i].Name == expectedResult[i].Name, errorMessagePrefix + " " + "Name is not returned properly.");
                Assert.True(actualResult[i].Code == expectedResult[i].Code, errorMessagePrefix + " " + "Code is not returned properly.");
                Assert.True(actualResult[i].Price == expectedResult[i].Price, errorMessagePrefix + " " + "Price is not returned properly.");
            }
        }

        [Fact]
        public async Task GetById_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ProceduresService GetById() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);
            var id = 1;

            // Act
            var actualResult = await proceduresService.GetById<ProcedureServiceModel>(id);
            var expectedResult = this.GetDummyData().First().To<ProcedureServiceModel>();

            // Assert
            Assert.True(actualResult.Id == expectedResult.Id, errorMessagePrefix + " " + "ProcedureServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetByName_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ProceduresService GetByName() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);


            var existentName = "Лечение на пулпит или периодонтит на постоянен зъб";

            // Act
            var actualResult = await proceduresService.GetByName<ProcedureServiceModel>(existentName);
            var expectedResult = this.GetDummyData().First().To<ProcedureServiceModel>();

            // Assert
            Assert.True(actualResult.Name == expectedResult.Name, errorMessagePrefix + " " + "ProcedureServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetByName_WithNonExistentname_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);

            var nonExistentTitle = "NonExistent";

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await proceduresService.GetByName<ProcedureServiceModel>(nonExistentTitle);
            });
        }

        [Fact]
        public async Task GetCount_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ProceduresService GetCount() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var proceduresRepository = new EfDeletableEntityRepository<Procedure>(context);
            var proceduresService = new ProceduresService(proceduresRepository);

            // Act
            var actualResult = proceduresService.GetCount();
            var expectedResult = 2;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix);
        }

        [Fact]
        public void GetCount_ShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Procedure>>();

            repository.Setup(r => r.All()).Returns(new List<Procedure>
                                                        {
                                                            new Procedure(),
                                                            new Procedure(),
                                                            new Procedure(),
                                                        }.AsQueryable());
            var service = new ProceduresService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        private List<Procedure> GetDummyData()
        {
            return new List<Procedure>()
            {
                new Procedure { Id = 1,  Name = "Лечение на пулпит или периодонтит на постоянен зъб", Code = Code.ThreeHundredThirtyThree, Price = 80 },
                new Procedure { Id = 2, Name = "Екстракция на постоянен зъб с анестезия", Code = Code.FiveHundredNine, Price = 35 },
            };
        }

        private async Task SeedDataAsync(ApplicationDbContext context)
        {
            context.AddRange(this.GetDummyData());
            await context.SaveChangesAsync();
        }
    }
}
