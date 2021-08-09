namespace DentalStudio.Web.ViewModels.Medicine.Doctors
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class DoctorProfilePhotoViewModel : IMapFrom<DoctorServiceModel>
    {
        public string FullName { get; set; }

        public string Photo { get; set; }
    }
}
