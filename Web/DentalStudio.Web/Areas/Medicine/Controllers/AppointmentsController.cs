namespace DentalStudio.Web.Areas.Medicine.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Medicine.Appointments;
    using DentalStudio.Web.ViewModels.Medicine.Doctors;
    using DentalStudio.Web.ViewModels.Medicine.Patients;
    using DentalStudio.Web.ViewModels.Medicine.Procedures;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentsController : MedicineController
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IDoctorsService doctorsService;
        private readonly IPatientsService patientsService;
        private readonly IProceduresService proceduresService;

        public AppointmentsController(
            IAppointmentsService appointmentsService,
            IDoctorsService doctorsService,
            IPatientsService patientsService,
            IProceduresService proceduresService)
        {
            this.appointmentsService = appointmentsService;
            this.doctorsService = doctorsService;
            this.patientsService = patientsService;
            this.proceduresService = proceduresService;
        }

        // List Of appointments
        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await this.doctorsService.GetDoctorById<DoctorViewModel>(userId);
            var appointmentsViewModel = this.appointmentsService.GetAllByDoctor<AppointmentDoctorViewModel>(doctor.Id);
            return this.View(appointmentsViewModel);
        }

        // Create Appointment
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var patients = this.patientsService.GetAll<PatientDoctorAppointmentViewModel>().ToList();
            var procedures = this.proceduresService.GetAll<ProcedureDoctorAppointmentViewModel>().ToList();

            var appointment = new DoctorAppointmentCreateModel
            {
                Patients = patients,
                Procedures = procedures,
            };

            return this.View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DoctorAppointmentCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Patients = this.patientsService.GetAll<PatientDoctorAppointmentViewModel>().ToList();
                model.Procedures = this.proceduresService.GetAll<ProcedureDoctorAppointmentViewModel>().ToList();
                return this.View(model);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await this.doctorsService.GetDoctorById<DoctorViewModel>(userId);
            var appointment = await this.appointmentsService.CreateByDoctor(model, doctor.Id);

            if (appointment != null)
            {
                this.TempData["InfoMessage"] = InfoMessages.CreateSuccessMessage;
                return this.RedirectToAction("All");
            }
            else
            {
                this.TempData["InfoMessage"] = ErrorMessages.CreateErrorMessage;
                return this.RedirectToAction("Create");
            }
        }

        // Delete Appointment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await this.appointmentsService.GetById<AppointmentServiceModel>(id);
            if (appointment == null)
            {
                return this.NotFound();
            }

            await this.appointmentsService.Delete(appointment);
            this.TempData["InfoMessage"] = InfoMessages.DeleteSuccessMessage;
            return this.RedirectToAction("All");
        }

        // Edit Appointment
        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await this.appointmentsService.GetById<AppointmentServiceModel>(id);
            if (appointment == null)
            {
                return this.NotFound();
            }

            var patients = this.patientsService.GetAll<PatientDoctorAppointmentViewModel>().ToList();
            var procedures = this.proceduresService.GetAll<ProcedureDoctorAppointmentViewModel>().ToList();

            var appointmentEditViewModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<DoctorAppointmentEditViewModel>(appointment);

            appointmentEditViewModel.Patients = patients;
            appointmentEditViewModel.Procedures = procedures;

            return this.View(appointmentEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorAppointmentEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await this.doctorsService.GetDoctorById<DoctorViewModel>(userId);
            var appointment = await this.appointmentsService.EditByDoctor(id, model, doctor.Id);

            if (appointment != null)
            {
                this.TempData["InfoMessage"] = InfoMessages.AppointmentEditSuccessMessage;
                return this.RedirectToAction("All");
            }
            else
            {
                this.TempData["InfoMessage"] = ErrorMessages.CreateErrorMessage;
                return this.RedirectToAction("Edit");
            }
        }

        // Appointment Details
        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await this.appointmentsService.GetById<DoctorAppointmentDetailsViewModel>(id);
            if (appointment == null)
            {
                return this.NotFound();
            }

            return this.View(appointment);
        }
    }
}
