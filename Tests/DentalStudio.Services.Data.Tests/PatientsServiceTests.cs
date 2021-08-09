namespace DentalStudio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

    public class PatientsServiceTests
    {
        [Fact]
        public async Task AddPatient_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PatientsService AddPatient() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);

            var patientServiceModel = AutoMapperConfig.MapperInstance.Map<PatientServiceModel>(new Patient
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                FullName = "Ivan Ivanov",
                Email = "ivan@ivan.bg",
                PhoneNumber = "1234567898",
                Address = "Plovdiv",
                Gender = Gender.Male,
                DateOfBirth = DateTime.ParseExact("15/06/1977", "dd/MM/yyyy", null),
                Photo = " ",
                BloodGroup = BloodGroup.APositive,
                IsAlergic = false,
                IsInsured = true,
                UserId = "1dfrrttyhhhre777888jjj",
            });

            // Act
            var result = await patientsService.AddPatient(patientServiceModel, patientServiceModel.UserId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddPatient_WithCorrectData_ShouldSuccessfulyCreate()
        {
            var errorMessagePrefix = "PatientsService AddPatient() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);

            var patientServiceModel = AutoMapperConfig.MapperInstance.Map<PatientServiceModel>(new Patient
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                FullName = "Ivan Ivanov",
                Email = "ivan@ivan.bg",
                PhoneNumber = "1234567898",
                Address = "Plovdiv",
                Gender = Gender.Male,
                DateOfBirth = DateTime.ParseExact("15/06/1977", "dd/MM/yyyy", null),
                Photo = " ",
                BloodGroup = BloodGroup.APositive,
                IsAlergic = false,
                IsInsured = true,
                UserId = "1dfrrttyhhhre777888jjj",
            });

            // Act
            var patientsCount = patientsRepository.All().Count();
            await patientsService.AddPatient(patientServiceModel, patientServiceModel.UserId);
            var actualResult = patientsRepository.All().Count();
            var expectedResult = patientsCount + 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Patients count is not incremented.");
        }

        [Fact]
        public async Task Edit_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PatientsService Edit() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);
            var patientServiceModel = patientsRepository.All().First().To<PatientServiceModel>();
            patientServiceModel.FirstName = "New First Name";

            // Act
            var actualResult = await patientsService.Edit(patientServiceModel.Id, patientServiceModel);
            var expectedResult = this.GetDummyData().First().To<PatientServiceModel>().Id;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "PatientServiceModel is not returned properly.");
        }

        //[Fact]
        //public async Task Delete_ShouldReturnCorrectResult()
        //{
        //    var errorMessagePrefix = "PatientsService Delete() method does not work properly.";

        //    // Arrange
        //    MapperInitializer.InitializeMapper();
        //    var context = ApplicationDbContextInMemoryFactory.InitializeContext();
        //    await this.SeedDataAsync(context);
        //    var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
        //    var patientsService = new PatientsService(patientsRepository);
        //    var patientServiceModel = patientsRepository.All().First().To<PatientServiceModel>();

        //    // Act
        //    var result = await patientsService.Delete(patientServiceModel);

        //    // Assert
        //    Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        //}

        //[Fact]
        //public async Task Delete_ShouldSuccessfullyDelete()
        //{
        //    var errorMessagePrefix = "PatientsService Delete() method does not work properly.";

        //    // Arrange
        //    MapperInitializer.InitializeMapper();
        //    var context = ApplicationDbContextInMemoryFactory.InitializeContext();
        //    await this.SeedDataAsync(context);
        //    var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
        //    var patientsService = new PatientsService(patientsRepository);
        //    var patientServiceModel = patientsRepository.All().First().To<PatientServiceModel>();

        //    // Act
        //    var patientsCount = patientsRepository.All().Count();
        //    await patientsService.Delete(patientServiceModel);
        //    var actualResult = patientsRepository.All().Count();
        //    var expectedResult = patientsCount - 1;

        //    // Assert
        //    Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Patients count is not reduced.");
        //}

        [Fact]
        public async Task GetAll_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PatientsService GetAll() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);

            // Act
            var actualResult = patientsService.GetAll<Patient>().ToList();
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
            var errorMessagePrefix = "PatientsService GetCount() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);

            // Act
            var actualResult = patientsService.GetCount();
            var expectedResult = 2;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix);
        }

        [Fact]
        public async Task GetById_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PatientsService GetById() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);
            var id = "123edfgrty67u";

            // Act
            var actualResult = await patientsService.GetById<PatientServiceModel>(id);
            var expectedResult = this.GetDummyData().First().To<PatientServiceModel>();

            // Assert
            Assert.True(actualResult.Id == expectedResult.Id, errorMessagePrefix + " " + "PatientServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetByName_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PatientsService GetByName() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);
            var existentName = "Dian Ivanov";

            // Act
            var actualResult = await patientsService.GetByName<PatientServiceModel>(existentName);
            var expectedResult = this.GetDummyData().First().To<PatientServiceModel>();

            // Assert
            Assert.True(actualResult.FullName == expectedResult.FullName, errorMessagePrefix + " " + "PatientServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetByName_WithNonExistentname_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);

            var nonExistentName = "NonExistent";

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await patientsService.GetByName<PatientServiceModel>(nonExistentName);
            });
        }

        [Fact]
        public async Task GetPatientById_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PatientsService GetPatientById() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);
            var userId = "1dfrrttyhhhre-kkjh4455gh";

            // Act
            var actualResult = await patientsService.GetPatientById<PatientServiceModel>(userId);
            var expectedResult = this.GetDummyData().First().To<PatientServiceModel>();

            // Assert
            Assert.True(actualResult.Id == expectedResult.Id, errorMessagePrefix + " " + "PatientServiceModel is not returned properly.");
        }

        [Fact]
        public async Task GetUrl_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PatientsService GetUrl() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            await this.SeedDataAsync(context);
            var patientsRepository = new EfDeletableEntityRepository<Patient>(context);
            var patientsService = new PatientsService(patientsRepository);
            var id = "123edfgrty67u";

            // Act
            var actualResult = patientsService.GetUrl(id);
            var expectedResult = this.GetDummyData().First().To<PatientServiceModel>().Photo;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "PhotoUrl is not returned properly.");
        }

        private List<Patient> GetDummyData()
        {
            return new List<Patient>()
            {
                new Patient
            {
                Id = "123edfgrty67u",
                FirstName = "Dian",
                LastName = "Ivanov",
                FullName = "Dian Ivanov",
                Email = "dian@dian.bg",
                PhoneNumber = "1234567898",
                Address = "Plovdiv",
                Gender = Gender.Male,
                DateOfBirth = DateTime.ParseExact("15/06/1977", "dd/MM/yyyy", null),
                Photo = GlobalConstants.DefaultPhoto,
                BloodGroup = BloodGroup.APositive,
                IsAlergic = false,
                IsInsured = true,
                UserId = "1dfrrttyhhhre-kkjh4455gh",
            },
                new Patient
            {
                Id = "3345wweeeww-33jjf-7767",
                FirstName = "Sonya",
                LastName = "Petrova",
                FullName = "Sonya Petrova",
                Email = "sonya@sonya.bg",
                PhoneNumber = "1234567898",
                Address = "Sofia",
                Gender = Gender.Female,
                DateOfBirth = DateTime.ParseExact("15/06/1996", "dd/MM/yyyy", null),
                Photo = GlobalConstants.DefaultPhoto,
                BloodGroup = BloodGroup.APositive,
                IsAlergic = false,
                IsInsured = false,
                UserId = "1dfrrttyhhhre-kkjhh0kiuu88",
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
