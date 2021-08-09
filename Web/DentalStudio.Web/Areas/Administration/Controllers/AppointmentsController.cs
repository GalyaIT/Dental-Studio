namespace DentalStudio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Appointments;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using DentalStudio.Web.ViewModels.Administration.Procedures;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentsController : AdministrationController
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

        // List Of Appointments
        public IActionResult All()
        {
            var appointmentsViewModel = this.appointmentsService.GetAll<AppointmentViewModel>().ToList();
            return this.View(appointmentsViewModel);
        }

        // Create Appointment
        [HttpGet(Name = "Create")]
        public async Task<ActionResult> Create()
        {
            var doctors = this.doctorsService.GetAll<DoctorAppointmentViewModel>().ToList();
            var patients = this.patientsService.GetAll<PatientAppointmentViewModel>().ToList();
            var procedures = this.proceduresService.GetAll<ProcedureAppointmentViewModel>().ToList();

            var appointment = new AppointmentCreateInputModel
            {
                Doctors = doctors,
                Patients = patients,
                Procedures = procedures,
            };

            return this.View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppointmentCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Doctors = this.doctorsService.GetAll<DoctorAppointmentViewModel>().ToList();
                model.Patients = this.patientsService.GetAll<PatientAppointmentViewModel>().ToList();
                model.Procedures = this.proceduresService.GetAll<ProcedureAppointmentViewModel>().ToList();
                return this.View(model);
            }

            var appointment = await this.appointmentsService.Create(model);
            if (appointment != null)
            {
                this.TempData["InfoMessage"] = string.Format(InfoMessages.AppointmentCreateSuccessMessage, model.DoctorName);
                return this.RedirectToAction("All");
            }
            else
            {
                this.TempData["InfoMessage"] = string.Format(ErrorMessages.AppointmentCreateErrorMessage, model.DoctorName);
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

            var doctors = this.doctorsService.GetAll<DoctorAppointmentViewModel>().ToList();
            var patients = this.patientsService.GetAll<PatientAppointmentViewModel>().ToList();
            var procedures = this.proceduresService.GetAll<ProcedureAppointmentViewModel>().ToList();

            var appointmentEditViewModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<AppointmentEditViewModel>(appointment);

            appointmentEditViewModel.Doctors = doctors;
            appointmentEditViewModel.Patients = patients;
            appointmentEditViewModel.Procedures = procedures;

            return this.View(appointmentEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appointment = await this.appointmentsService.Edit(id, model);

            if (appointment != null)
            {
                this.TempData["InfoMessage"] = string.Format(InfoMessages.EditAppointmentSuccessMessage, model.DoctorName);
                return this.RedirectToAction("All");
            }
            else
            {
                this.TempData["InfoMessage"] = string.Format(ErrorMessages.EditErrorMessage, model.DoctorName);
                return this.RedirectToAction("Edit");
            }
        }

        // Appointment Details
        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await this.appointmentsService.GetById<AppointmentDetailsViewModel>(id);
            if (appointment == null)
            {
                return this.NotFound();
            }

            return this.View(appointment);
        }
    }
}
