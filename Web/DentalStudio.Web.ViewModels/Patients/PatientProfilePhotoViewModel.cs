namespace DentalStudio.Web.ViewModels.Patients
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class PatientProfilePhotoViewModel : IMapFrom<PatientServiceModel>
    {
        public string FullName { get; set; }

        public string Photo { get; set; }
    }
}
