namespace DentalStudio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Data;
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Data.Repositories;
    using DentalStudio.Services.Data.Tests.Common;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Xunit;

    public class DoctorsServiceTests
    {
        [Fact]
        public async Task AddDoctor_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService AddDoctor() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);

            var doctorServiceModel = AutoMapperConfig.MapperInstance.Map<DoctorServiceModel>(new Doctor
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                FullName = "Ivan Ivanov",
                Email = "ivan@ivan.bg",
                PhoneNumber = "1234567898",
                Address = "Plovdiv",
                Gender = Gender.Male,
                DateOfBirth = DateTime.ParseExact("15/06/1977", "dd/MM/yyyy", null),
                Grade = " ",
                Photo = " ",
                Specialty = "ortodontist",
                UserId = "1dfrrttyhhhre-kkjhhg-68",
            });

            // Act
            var result = await doctorsService.AddDoctor(doctorServiceModel, doctorServiceModel.UserId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddDoctor_WithCorrectData_ShouldSuccessfulyCreate()
        {
            var errorMessagePrefix = "DoctorsService AddDoctor() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);

            var doctorServiceModel = AutoMapperConfig.MapperInstance.Map<DoctorServiceModel>(new Doctor
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                FullName = "Ivan Ivanov",
                Email = "ivan@ivan.bg",
                PhoneNumber = "1234567898",
                Address = "Plovdiv",
                Gender = Gender.Male,
                DateOfBirth = DateTime.ParseExact("15/06/1977", "dd/MM/yyyy", null),
                Grade = " ",
                Photo = " ",
                Specialty = "ortodontist",
                UserId = "1dfrrttyhhhre-kkjhhg-68",
            });

            // Act
            var doctorsCount = doctorsRepository.All().Count();
            await doctorsService.AddDoctor(doctorServiceModel, doctorServiceModel.UserId);
            var actualResult = doctorsRepository.All().Count();
            var expectedResult = doctorsCount + 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Doctors count is not incremented.");
        }

        [Fact]
        public async Task Edit_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService Edit() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);
            var doctorServiceModel = doctorsRepository.All().First().To<DoctorServiceModel>();
            doctorServiceModel.FirstName = "New First Name";

            // Act
            var actualResult = await doctorsService.Edit(doctorServiceModel.Id, doctorServiceModel);
            var expectedResult = this.GetDummyData().First().To<DoctorServiceModel>().Id;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "DoctorServiceModel is not returned properly.");
        }

        [Fact]
        public async Task Delete_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService Delete() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);
            var doctorServiceModel = doctorsRepository.All().First().To<DoctorServiceModel>();

            // Act
            var result = await doctorsService.Delete(doctorServiceModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task Delete_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "DoctorsService Delete() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);
            var doctorServiceModel = doctorsRepository.All().First().To<DoctorServiceModel>();

            // Act
            var doctorsCount = doctorsRepository.All().Count();
            await doctorsService.Delete(doctorServiceModel);
            var actualResult = doctorsRepository.All().Count();
            var expectedResult = doctorsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Doctors count is not reduced.");
        }

        [Fact]
        public async Task GetAll_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService GetAll() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);

            // Act
            var actualResult = doctorsService.GetAll<Doctor>().ToList();
            var expectedResult = this.GetDummyData().ToList();

            // Assert
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.True(actualResult[i].FirstName == expectedResult[i].FirstName, errorMessagePrefix + " " + "FirstName is not returned properly.");
                Assert.True(actualResult[i].LastName == expectedResult[i].LastName, errorMessagePrefix + " " + "LastName is not returned properly.");
                Assert.True(actualResult[i].Email == expectedResult[i].Email, errorMessagePrefix + " " + "Email is not returned properly.");
                Assert.True(actualResult[i].PhoneNumber == expectedResult[i].PhoneNumber, errorMessagePrefix + " " + "PhoneNumber is not returned properly.");
            }
        }

        [Fact]
        public async Task GetCount_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService GetCount() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);

            // Act
            var actualResult = doctorsService.GetCount();
            var expectedResult = 2;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix);
        }

        [Fact]
        public async Task GetById_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService GetById() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);
            var id = "3345wweeeww-hhdjjf-7767";

            // Act
            var actualResult = await doctorsService.GetById<DoctorServiceModel>(id);
            var expectedResult = this.GetDummyData().Skip(1).First().To<DoctorServiceModel>();

            // Assert
            Assert.True(actualResult.Id == expectedResult.Id, errorMessagePrefix + " " + "DoctorServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetByName_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService GetByName() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);
            var existentName = "Ivan Ivanov";

            // Act
            var actualResult = await doctorsService.GetByName<DoctorServiceModel>(existentName);
            var expectedResult = this.GetDummyData().First().To<DoctorServiceModel>();

            // Assert
            Assert.True(actualResult.FullName == expectedResult.FullName, errorMessagePrefix + " " + "DoctorServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetByName_WithNonExistentname_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);

            var nonExistentName = "NonExistent";

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await doctorsService.GetByName<DoctorServiceModel>(nonExistentName);
            });
        }

        [Fact]
        public async Task GetDoctorById_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService GetDoctorById() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);
            var userId = "1dfrrttyhhhre-kkjhhg-68";

            // Act
            var actualResult = await doctorsService.GetDoctorById<DoctorServiceModel>(userId);
            var expectedResult = this.GetDummyData().First().To<DoctorServiceModel>();

            // Assert
            Assert.True(actualResult.Id == expectedResult.Id, errorMessagePrefix + " " + "DoctorServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetUrl_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "DoctorsService GetUrl() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var doctorsRepository = new EfDeletableEntityRepository<Doctor>(context);
            var doctorsService = new DoctorsService(doctorsRepository);
            var id = "3345-hhdjjf-7767";

            // Act
            var actualResult = doctorsService.GetUrl(id);
            var expectedResult = this.GetDummyData().First().To<DoctorServiceModel>().Photo;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "PhotoUrl is not returned properly.");
        }

        private List<Doctor> GetDummyData()
        {
            return new List<Doctor>()
            {
                new Doctor
            {
                Id = "3345-hhdjjf-7767",
                FirstName = "Ivan",
                LastName = "Ivanov",
                FullName = "Ivan Ivanov",
                Email = "ivan@ivan.bg",
                PhoneNumber = "1234567898",
                Address = "Plovdiv",
                Gender = Gender.Male,
                DateOfBirth = DateTime.ParseExact("15/06/1977", "dd/MM/yyyy", null),
                Grade = " ",
                Photo = GlobalConstants.DefaultPhoto,
                Specialty = "ortodontist",
                UserId = "1dfrrttyhhhre-kkjhhg-68",
            },
                new Doctor
            {
                Id = "3345wweeeww-hhdjjf-7767",
                FirstName = "Ani",
                LastName = "Petrova",
                FullName = "Ani Petrova",
                Email = "ani@ani.bg",
                PhoneNumber = "1234567898",
                Address = "Sofia",
                Gender = Gender.Female,
                DateOfBirth = DateTime.ParseExact("15/06/1996", "dd/MM/yyyy", null),
                Grade = " ",
                Photo = GlobalConstants.DefaultPhoto,
                Specialty = "endodontist",
                UserId = "1dfrrttyhhhre-kkjhhg-77",
            },
            };
        }

        private async Task SeedDataAsync(ApplicationDbContext context)
        {
            context.AddRange(this.GetDummyData());
            await context.SaveChangesAsync();
        }
    }
}
