namespace DentalStudio.Web.ViewModels.Administration.Procedures
{
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class ProcedureAppointmentViewModel : IMapFrom<ProcedureServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
