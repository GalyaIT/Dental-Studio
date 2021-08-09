namespace DentalStudio.Web.ViewModels.Administration.Doctors
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class DoctorAppointmentViewModel : IMapFrom<DoctorServiceModel>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
