namespace DentalStudio.Web.Areas.Administration.Controllers
{
    using DentalStudio.Services.Data;
    using DentalStudio.Web.ViewModels.Administration.Appointments;
    using DentalStudio.Web.ViewModels.Administration.Dashboard;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using DentalStudio.Web.ViewModels.Administration.Procedures;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class DashboardController : AdministrationController
    {
        private readonly IDoctorsService doctorsService;
        private readonly IPatientsService patientsService;
        private readonly IProceduresService proceduresService;
        private readonly IAppointmentsService appointmentsService;

        public DashboardController(
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

        public IActionResult Index()
        {
            var doctors = this.doctorsService.GetAll<DoctorViewModel>().ToList();
            var patients = this.patientsService.GetAll<PatientViewModel>().ToList();
            var procedures = this.proceduresService.GetAll<ProcedureViewModel>().ToList();
            var appointments = this.appointmentsService.GetAll<AppointmentViewModel>().ToList();
            var dashboardViewModel = new DashboardViewModel
            {
                Doctors = doctors,
                Patients = patients,
                Procedures = procedures,
                Appointments = appointments,
            };
            return this.View(dashboardViewModel);
        }

        [HttpGet(Name = "Profile")]
        public IActionResult Profile()
        {
            return this.View();
        }
    }
}
