namespace DentalStudio.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Services;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Medicine.Doctors;
    using DentalStudio.Web.ViewModels.Medicine.Procedures;
    using DentalStudio.Web.ViewModels.Patients;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.PatientRoleName)]
    public class PatientsController : BaseController
    {

        private readonly IPatientsService patientsService;
        private readonly IDoctorsService doctorsService;
        private readonly IAppointmentsService appointmentsService;
        private readonly IProceduresService proceduresService;
        private readonly ICloudinaryService cloudinaryService;

        public PatientsController(
            IPatientsService patientsService,
            IDoctorsService doctorsService,
            IAppointmentsService appointmentsService,
            IProceduresService proceduresService,
            ICloudinaryService cloudinaryService)
        {
            this.patientsService = patientsService;
            this.doctorsService = doctorsService;
            this.appointmentsService = appointmentsService;
            this.proceduresService = proceduresService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await this.patientsService.GetPatientById<PatientViewModel>(userId);
            var appointments = this.appointmentsService.GetAllAppointmentsByPatient<PatientAppointmentModel>(patient.Id).ToList();
            var dashboardViewModel = new PatientIndexViewModel
            {
                Appointments = appointments,
            };
            return this.View(dashboardViewModel);
        }

        // Profile Details
        [HttpGet(Name = "Profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await this.patientsService.GetPatientById<PatientProfileDetailsViewModel>(userId);
            if (patient.Photo == null)
            {
                patient.Photo = GlobalConstants.DefaultPhoto;
            }

            return this.View(patient);
        }

        // Update Patient Profile
        [HttpGet(Name = "UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(string id)
        {
            var patient = await this.patientsService.GetById<PatientServiceModel>(id);
            if (patient == null)
            {
                return this.NotFound();
            }

            var patientProfileEditViewModel = AutoMapperConfig.MapperInstance.Map<PatientProfileUpdateViewModel>(patient);
            return this.View(patientProfileEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(PatientProfileUpdateViewModel patientUpdateViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(patientUpdateViewModel);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patientDb = await this.patientsService.GetPatientById<PatientServiceModel>(userId);

            var url = this.patientsService.GetUrl(patientDb.Id);

            string photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                patientUpdateViewModel.Photo,
                patientUpdateViewModel.FirstName,
                GlobalConstants.CloudFolderForDoctorsPhotos);

            var patientServiceModel = AutoMapperConfig.MapperInstance.Map<PatientServiceModel>(patientUpdateViewModel);
            if (patientUpdateViewModel.Photo == null)
            {
                patientServiceModel.Photo = url;
            }
            else
            {
                patientServiceModel.Photo = photoUrl;
            }

            var patient = await this.patientsService.UpdateProfile(userId, patientServiceModel);

            if (patient != null)
            {
                this.TempData["InfoMessage"] = InfoMessages.UpdateSuccessMessage;
                return this.Redirect("/Patients/Index");
            }
            else
            {
                this.TempData["InfoMessage"] = ErrorMessages.UpdateErrorMessage;
                return this.RedirectToAction("UpdateProfile");
            }
        }

        // List Of Appointments
        public async Task<IActionResult> Appointments()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await this.patientsService.GetPatientById<PatientViewModel>(userId);
            var appointmentsViewModel = this.appointmentsService.GetAllAppointmentsByPatient<PatientAppointmentModel>(patient.Id);
            return this.View(appointmentsViewModel);
        }

        // Add Appointment
        [HttpGet]
        public async Task<ActionResult> AddAppointment()
        {
            var doctors = this.doctorsService.GetAll<DoctorPatientAppointmentViewModel>().ToList();
            var procedures = this.proceduresService.GetAll<ProcedureDoctorAppointmentViewModel>().ToList();

            var appointment = new PatientAppointmentCreateModel
            {
                Doctors = doctors,
                Procedures = procedures,
            };

            return this.View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAppointment(PatientAppointmentCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Doctors = this.doctorsService.GetAll<DoctorPatientAppointmentViewModel>().ToList();
                model.Procedures = this.proceduresService.GetAll<ProcedureDoctorAppointmentViewModel>().ToList();
                return this.View(model);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await this.patientsService.GetPatientById<PatientViewModel>(userId);
            var appointment = await this.appointmentsService.CreateByPatient(model, patient.Id);

            if (appointment != null)
            {
                this.TempData["InfoMessage"] = InfoMessages.CreateSuccessMessage;
                return this.RedirectToAction("Appointments");
            }
            else
            {
                this.TempData["InfoMessage"] = ErrorMessages.CreateErrorMessage;
                return this.RedirectToAction("AddAppointment");
            }
        }

        // Add Appointment from doctors page
        [HttpGet]
        public async Task<ActionResult> AddAppointmentByDoctor(string id)
        {
            var doctor = await this.doctorsService.GetById<DoctorPatientAppointmentViewModel>(id);
            var procedures = this.proceduresService.GetAll<ProcedureDoctorAppointmentViewModel>().ToList();

            var appointment = new PatientAppointmentCreateModel
            {
                DoctorName = doctor.FullName,
                Procedures = procedures,
            };

            return this.View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAppointmentByDoctor(PatientAppointmentCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Procedures = this.proceduresService.GetAll<ProcedureDoctorAppointmentViewModel>().ToList();
                return this.View(model);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await this.patientsService.GetPatientById<PatientViewModel>(userId);
            var appointment = await this.appointmentsService.CreateByPatient(model, patient.Id);

            if (appointment != null)
            {
                this.TempData["InfoMessage"] = InfoMessages.CreateSuccessMessage;
                return this.RedirectToAction("Appointments");
            }
            else
            {
                this.TempData["InfoMessage"] = ErrorMessages.CreateErrorMessage;
                return this.RedirectToAction("AddAppointmentByDoctor");
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
            return this.RedirectToAction("Appointments");
        }

        // Appointment Details
        [HttpGet(Name = "AppointmentDetails")]
        public async Task<IActionResult> AppointmentDetails(int id)
        {
            var appointment = await this.appointmentsService.GetById<PatientAppointmentDetailsViewModel>(id);
            if (appointment == null)
            {
                return this.NotFound();
            }

            return this.View(appointment);
        }

        [Authorize]
        public async Task<IActionResult> Doctors()
        {
            var doctors = this.doctorsService.GetAll<DoctorViewModel>().ToList();
            return this.View(doctors);
        }

        // Doctor Details
        [HttpGet(Name = "DoctorDetails")]
        [AllowAnonymous]
        public async Task<IActionResult> DoctorDetails(string id)
        {
            var doctor = await this.doctorsService.GetById<PatientDoctorDetailsViewModel>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            return this.View(doctor);
        }
    }
}
