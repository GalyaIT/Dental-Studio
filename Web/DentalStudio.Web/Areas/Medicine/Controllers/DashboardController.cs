namespace DentalStudio.Web.Areas.Medicine.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DentalStudio.Services.Data;
    using DentalStudio.Web.ViewModels.Medicine.Appointments;
    using DentalStudio.Web.ViewModels.Medicine.Dashboard;
    using DentalStudio.Web.ViewModels.Medicine.Doctors;
    using DentalStudio.Web.ViewModels.Medicine.Patients;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : MedicineController
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

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await this.doctorsService.GetDoctorById<DoctorViewModel>(userId);
            var patients = this.appointmentsService.GetAllPatientByDoctor<PatientDoctorViewModel>(doctor.Id).ToList();
            var waitingAppointments = this.appointmentsService.GetAllWaitingByDoctor<AppointmentDoctorViewModel>(doctor.Id).ToList();
            var confirmedAppointments = this.appointmentsService.GetAllConfirmedByDoctor<AppointmentDoctorViewModel>(doctor.Id).ToList();
            var appointments = this.appointmentsService.GetAllByDoctor<AppointmentDoctorViewModel>(doctor.Id).ToList();
            var dashboardViewModel = new DashboardViewModel
            {
                Patients = patients,
                WaitingAppointments = waitingAppointments,
                ConfirmedAppointments = confirmedAppointments,
                AllAppointments = appointments,
            };

            return this.View(dashboardViewModel);
        }
    }
}
