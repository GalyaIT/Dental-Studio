namespace DentalStudio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DentalStudio.Data.Models;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Patients;

    public interface IPatientsService
    {
        Task<bool> Create(PatientCreateInputModel patientCreateInputModel);

        IEnumerable<T> GetAll<T>();

        Task<PatientServiceModel> Delete(PatientServiceModel patient);

        Task<T> GetById<T>(string id);

        Task<string> Edit(string id, PatientServiceModel patient);

        Task<T> GetByName<T>(string name);

        string GetUrl(string id);

        Task<T> GetPatientById<T>(string userId);

        Task<string> UpdateProfile(string userId, PatientServiceModel patientServiceModel);

        int GetCount();

        Task<bool> AddPatient(PatientServiceModel patientServiceModel, string userId);
    }
}
