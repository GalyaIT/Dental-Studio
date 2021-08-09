namespace DentalStudio.Web.Areas.Medicine.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DentalStudio.Services.Data;
    using DentalStudio.Web.ViewModels.Medicine.Appointments;
    using DentalStudio.Web.ViewModels.Medicine.Doctors;
    using DentalStudio.Web.ViewModels.Medicine.Patients;
    using Microsoft.AspNetCore.Mvc;

    public class PatientsController : MedicineController
    {
        private readonly IPatientsService patientsService;
        private readonly IDoctorsService doctorsService;
        private readonly IAppointmentsService appointmentsService;

        public PatientsController(
            IPatientsService patientsService,
            IDoctorsService doctorsService,
            IAppointmentsService appointmentsService)
        {
            this.patientsService = patientsService;
            this.doctorsService = doctorsService;
            this.appointmentsService = appointmentsService;
        }

        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await this.doctorsService.GetDoctorById<DoctorViewModel>(userId);
            var patients = this.appointmentsService.GetAllPatientByDoctor<PatientDoctorViewModel>(doctor.Id).ToList();

            return this.View(patients);
        }

        // Patient Details
        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await this.doctorsService.GetDoctorById<DoctorViewModel>(userId);
            var appointments = this.appointmentsService.GetAllAppointmentsByPatientAndDoctor<AppointmentDoctorViewModel>(id, doctor.Id);
            var patient = await this.patientsService.GetById<PatientDoctorViewModel>(id);
            if (patient == null)
            {
                return this.NotFound();
            }

            patient.Appointments = appointments;
            return this.View(patient);
        }
    }
}
