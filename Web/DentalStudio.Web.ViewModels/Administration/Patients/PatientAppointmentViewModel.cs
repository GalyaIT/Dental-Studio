namespace DentalStudio.Web.ViewModels.Administration.Patients
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class PatientAppointmentViewModel : IMapFrom<PatientServiceModel>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
