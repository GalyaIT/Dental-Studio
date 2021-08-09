namespace DentalStudio.Web.ViewComponents
{
    using System.Threading.Tasks;

    using DentalStudio.Services.Data;
    using DentalStudio.Web.ViewComponents.Models;
    using Microsoft.AspNetCore.Mvc;

    public class StatisticViewComponent : ViewComponent
    {
        private readonly IDoctorsService doctorsService;
        private readonly IPatientsService patientsService;
        private readonly IProceduresService proceduresService;
        private readonly IAppointmentsService appointmentsService;

        public StatisticViewComponent(
            IDoctorsService doctorsService,
            IPatientsService patientsService,
            IProceduresService proceduresService,
            IAppointmentsService appointmentsService)
        {
            this.doctorsService = doctorsService;
            this.patientsService = patientsService;
            this.proceduresService = proceduresService;
            this.appointmentsService = appointmentsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new StatisticModel();
            model.Doctors = this.doctorsService.GetCount();
            model.Patients = this.patientsService.GetCount();
            model.Procedures = this.proceduresService.GetCount();
            model.Appointments = this.appointmentsService.GetCount();

            return this.View(model);
        }
    }
}
