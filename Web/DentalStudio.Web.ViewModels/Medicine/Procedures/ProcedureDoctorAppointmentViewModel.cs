namespace DentalStudio.Web.ViewModels.Medicine.Procedures
{
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class ProcedureDoctorAppointmentViewModel : IMapFrom<ProcedureServiceModel>/*, IHaveCustomMappings*/
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
