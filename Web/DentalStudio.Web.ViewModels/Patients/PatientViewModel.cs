namespace DentalStudio.Web.ViewModels.Patients
{
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class PatientViewModel : IMapFrom<PatientServiceModel>
    {
        public string Id { get; set; }
    }
}
