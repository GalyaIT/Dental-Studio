namespace DentalStudio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DentalStudio.Services.Models;

    public interface IDoctorsService
    {
        Task<T> GetById<T>(string id);

        IEnumerable<T> GetAll<T>();

        Task<bool> Delete(DoctorServiceModel doctor);

        Task<string> Edit(string id, DoctorServiceModel doctor);

        Task<T> GetByName<T>(string name);

        Task<T> GetDoctorById<T>(string userId);

        string GetUrl(string id);

        int GetCount();

        Task<bool> AddDoctor(DoctorServiceModel doctorServiceModel, string userId);
    }
}
