namespace DentalStudio.Web.ViewModels.About
{
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class DoctorViewModel : IMapFrom<DoctorServiceModel>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Specialty { get; set; }

        public string Photo { get; set; }
    }
}
