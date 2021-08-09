namespace DentalStudio.Web.ViewModels.Medicine.Patients
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class PatientDoctorAppointmentViewModel : IMapFrom<PatientServiceModel>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
