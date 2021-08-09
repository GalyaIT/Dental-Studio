namespace DentalStudio.Services.Data
{
    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DoctorsService : IDoctorsService
    {
        private readonly IDeletableEntityRepository<Doctor> doctorsRepository;

        public DoctorsService(IDeletableEntityRepository<Doctor> doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        public async Task<bool> AddDoctor(DoctorServiceModel model, string userId)
        {
            var doctor = new Doctor
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FullName = model.FirstName + " " + model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Grade = model.Grade,
                Photo = model.Photo,
                Specialty = model.Specialty,
                UserId = userId,
            };
            await this.doctorsRepository.AddAsync(doctor);
            var result = await this.doctorsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(DoctorServiceModel doctorServiceModel)
        {
            Doctor doctor = await this.doctorsRepository.All().FirstOrDefaultAsync(doctor => doctor.Id == doctorServiceModel.Id);

            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor));
            }

            this.doctorsRepository.Delete(doctor);

            int result = await this.doctorsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<string> Edit(string id, DoctorServiceModel doctorEditViewModel)
        {
            Doctor doctor = this.doctorsRepository.All().FirstOrDefault(doctor => doctor.Id == id);

            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor));
            }

            doctor.FirstName = doctorEditViewModel.FirstName;
            doctor.LastName = doctorEditViewModel.LastName;
            doctor.FullName = doctorEditViewModel.FirstName + " " + doctorEditViewModel.LastName;
            doctor.Email = doctorEditViewModel.Email;
            doctor.PhoneNumber = doctorEditViewModel.PhoneNumber;
            doctor.Address = doctorEditViewModel.Address;
            doctor.Gender = doctorEditViewModel.Gender;
            doctor.DateOfBirth = doctorEditViewModel.DateOfBirth;
            doctor.Grade = doctorEditViewModel.Grade;
            doctor.Photo = doctorEditViewModel.Photo;
            doctor.Specialty = doctorEditViewModel.Specialty;

            this.doctorsRepository.Update(doctor);
            int result = await this.doctorsRepository.SaveChangesAsync();

            return doctor.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var query =
                 this.doctorsRepository.All().To<DoctorServiceModel>();

            return query.To<T>().ToList();
        }

        public async Task<T> GetById<T>(string id)
        {

            var doctor = await this.doctorsRepository
               .All()
               .Where(d => d.Id == id).To<DoctorServiceModel>()
               .FirstOrDefaultAsync();

            var doctorServiceModel = AutoMapperConfig.MapperInstance.Map<T>(doctor);

            return doctorServiceModel;
        }

        public async Task<T> GetByName<T>(string name)
        {
            var doctor = await this.doctorsRepository
             .All()
             .Where(d => d.FullName == name).To<DoctorServiceModel>()
             .FirstOrDefaultAsync();

            if (doctor == null)
            {
                throw new ArgumentNullException(
                    string.Format(nameof(doctor)));
            }

            var doctorServiceModel = AutoMapperConfig.MapperInstance.Map<T>(doctor);

            return doctorServiceModel;
        }

        public int GetCount()
        {
            return this.doctorsRepository.All().Count();
        }

        public async Task<T> GetDoctorById<T>(string userId)
        {
            var doctor = await this.doctorsRepository
              .All()
              .Where(d => d.UserId == userId).To<DoctorServiceModel>()
              .FirstOrDefaultAsync();

            var doctorServiceModel = AutoMapperConfig.MapperInstance.Map<T>(doctor);

            return doctorServiceModel;
        }

        public string GetUrl(string id)
        {
            var doctor = this.doctorsRepository.All().FirstOrDefault(d => d.Id == id);
            var url = doctor.Photo;
            return url;
        }
    }
}
