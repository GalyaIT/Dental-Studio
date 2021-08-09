namespace DentalStudio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using Microsoft.EntityFrameworkCore;

    public class PatientsService : IPatientsService
    {
        private readonly IDeletableEntityRepository<Patient> patientsRepository;

        public PatientsService(IDeletableEntityRepository<Patient> patientsRepository)
        {
            this.patientsRepository = patientsRepository;
        }

        public async Task<bool> AddPatient(PatientServiceModel model, string userId)
        {
            var patient = new Patient
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FullName = model.FirstName + " " + model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Photo = model.Photo,
                BloodGroup = model.BloodGroup,
                Age = model.Age,
                IsAlergic = model.IsAlergic,
                IsInsured = model.IsInsured,
                UserId = userId,
            };
            await this.patientsRepository.AddAsync(patient);
            var result = await this.patientsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Create(PatientCreateInputModel patientCreateInputModel)
        {
            Patient patient = new Patient
            {
                FirstName = patientCreateInputModel.FirstName,
                LastName = patientCreateInputModel.LastName,
                FullName = patientCreateInputModel.FirstName + " " + patientCreateInputModel.LastName,
                Email = patientCreateInputModel.Email,
                UserId = patientCreateInputModel.UserId,
            };

            await this.patientsRepository.AddAsync(patient);
            var result = await this.patientsRepository.SaveChangesAsync();
            return result > 0;
        }

        public async Task<PatientServiceModel> Delete(PatientServiceModel patientServiceModel)
        {
            Patient patient = await this.patientsRepository.All()
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(patient => patient.Id == patientServiceModel.Id);

            if (patient.Appointments.Count == 0)
            {
                this.patientsRepository.Delete(patient);
                var result = await this.patientsRepository.SaveChangesAsync();
                return null;
            }
            else
            {
                patient.IsDeleted = true;
                this.patientsRepository.Update(patient);
                await this.patientsRepository.SaveChangesAsync();
                return patient.To<PatientServiceModel>();
            }

            //Patient patient = await this.patientsRepository.All().FirstOrDefaultAsync(patient => patient.Id == patientServiceModel.Id);

            //if (patient == null)
            //{
            //    throw new ArgumentNullException(nameof(patient));
            //}

            //this.patientsRepository.Delete(patient);

            //int result = await this.patientsRepository.SaveChangesAsync();

            //return result > 0;
        }

        public async Task<string> Edit(string id, PatientServiceModel patientServiceModel)
        {
            Patient patient = this.patientsRepository.All().FirstOrDefault(patient => patient.Id == id);

            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            patient.FirstName = patientServiceModel.FirstName;
            patient.LastName = patientServiceModel.LastName;
            patient.FullName = patientServiceModel.FirstName + " " + patientServiceModel.LastName;
            patient.Email = patientServiceModel.Email;
            patient.PhoneNumber = patientServiceModel.PhoneNumber;
            patient.Address = patientServiceModel.Address;
            patient.Gender = patientServiceModel.Gender;
            patient.DateOfBirth = patientServiceModel.DateOfBirth;
            patient.Photo = patientServiceModel.Photo;
            patient.BloodGroup = patientServiceModel.BloodGroup;
            patient.Age = patientServiceModel.Age;
            patient.IsAlergic = patientServiceModel.IsAlergic;
            patient.IsInsured = patientServiceModel.IsInsured;

            this.patientsRepository.Update(patient);
            int result = await this.patientsRepository.SaveChangesAsync();

            return patient.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var query =
                 this.patientsRepository.All().To<PatientServiceModel>();

            return query.To<T>().ToList();
        }

        public async Task<T> GetById<T>(string id)
        {

            var patient = await this.patientsRepository
               .All()
               .Where(d => d.Id == id).To<PatientServiceModel>()
               .FirstOrDefaultAsync();
            var patientServiceModel = AutoMapperConfig.MapperInstance.Map<T>(patient);

            return patientServiceModel;
        }

        public async Task<T> GetByName<T>(string name)
        {
            var patient = await this.patientsRepository
             .All()
             .Where(d => d.FullName == name).To<PatientServiceModel>()
             .FirstOrDefaultAsync();

            if (patient == null)
            {
                throw new ArgumentNullException(
                    string.Format(nameof(patient)));
            }

            var patientServiceModel = AutoMapperConfig.MapperInstance.Map<T>(patient);

            return patientServiceModel;
        }

        public int GetCount()
        {
            return this.patientsRepository.All().Count();
        }

        public async Task<T> GetPatientById<T>(string userId)
        {
            var patient = await this.patientsRepository
              .All()
              .Where(p => p.UserId == userId).To<PatientServiceModel>()
              .FirstOrDefaultAsync();

            if (patient == null)
            {
                throw new ArgumentNullException(
                    string.Format(nameof(patient)));
            }

            var patientServiceModel = AutoMapperConfig.MapperInstance.Map<T>(patient);

            return patientServiceModel;
        }

        public string GetUrl(string id)
        {
            var patient = this.patientsRepository.All().FirstOrDefault(p => p.Id == id);
            var url = patient.Photo;
            return url;
        }

        public async Task<string> UpdateProfile(string userId, PatientServiceModel patientServiceModel)
        {
            Patient patient = this.patientsRepository.All().FirstOrDefault(patient => patient.UserId == userId);

            //if (patient == null)
            //{
            //    throw new ArgumentNullException(nameof(patient));
            //}

            patient.FirstName = patientServiceModel.FirstName;
            patient.LastName = patientServiceModel.LastName;
            patient.FullName = patientServiceModel.FirstName + " " + patientServiceModel.LastName;
            patient.Email = patientServiceModel.Email;
            patient.PhoneNumber = patientServiceModel.PhoneNumber;
            patient.Address = patientServiceModel.Address;
            patient.Gender = patientServiceModel.Gender;
            patient.DateOfBirth = patientServiceModel.DateOfBirth;
            patient.Photo = patientServiceModel.Photo;
            patient.BloodGroup = patientServiceModel.BloodGroup;
            patient.Age = patientServiceModel.Age;
            patient.IsAlergic = patientServiceModel.IsAlergic;
            patient.IsInsured = patientServiceModel.IsInsured;


            this.patientsRepository.Update(patient);
            int result = await this.patientsRepository.SaveChangesAsync();

            return patient.Id;
        }
    }
}
