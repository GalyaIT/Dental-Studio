namespace DentalStudio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Appointments;
    using DentalStudio.Web.ViewModels.Medicine.Appointments;
    using DentalStudio.Web.ViewModels.Patients;

    public interface IAppointmentsService
    {
        Task<AppointmentServiceModel> Create(AppointmentCreateInputModel appointment);

        Task<string> Edit(int id, AppointmentEditViewModel appointment);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByDoctor<T>(string userId);

        IEnumerable<T> GetAllWaitingByDoctor<T>(string userId);

        IEnumerable<T> GetAllConfirmedByDoctor<T>(string userId);

        IEnumerable<T> GetAllRefusedByDoctor<T>(string userId);

        IEnumerable<T> GetAllPatientByDoctor<T>(string id);

        Task<T> GetById<T>(int id);

        Task<bool> Delete(AppointmentServiceModel appointment);

        Task<AppointmentServiceModel> CreateByDoctor(DoctorAppointmentCreateModel appointment, string id);

        Task<string> EditByDoctor(int id, DoctorAppointmentEditViewModel appointment, string doctorId);

        IEnumerable<T> GetAllAppointmentsByPatient<T>(string id);

        Task<AppointmentServiceModel> CreateByPatient(PatientAppointmentCreateModel appointment, string id);

        int GetCount();

        IEnumerable<T> GetAllAppointmentsByPatientAndDoctor<T>(string id, string doctorId);
    }
}
