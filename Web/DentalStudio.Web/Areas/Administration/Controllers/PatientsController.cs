namespace DentalStudio.Web.Areas.Administration.Controllers
{
    using DentalStudio.Common;
    using DentalStudio.Data.Models;
    using DentalStudio.Services;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Accounts;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class PatientsController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPatientsService patientsService;
        private readonly ICloudinaryService cloudinaryService;

        public PatientsController(
            UserManager<ApplicationUser> userManager,
            IPatientsService patientsService,
            ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.patientsService = patientsService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.PatientRoleName);

                return this.RedirectToAction("AddPatient", "Patients", new { id = user.Id });
            }

            return this.NotFound();
        }

        // Add Patient
        [HttpGet]
        public async Task<IActionResult> AddPatient(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.NotFound();
            }

            var patientCreateInputModel = new PatientCreateInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserId = user.Id,
            };
            return this.View(patientCreateInputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPatient(string id, PatientCreateInputModel patientCreateModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(patientCreateModel);
            }

            string photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                 patientCreateModel.Photo,
                 patientCreateModel.FirstName,
                 GlobalConstants.CloudFolderForDoctorsPhotos);

            var patientServiceModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<PatientServiceModel>(patientCreateModel);
            patientServiceModel.Photo = photoUrl;

            await this.patientsService.AddPatient(patientServiceModel, id);
            this.TempData["InfoMessage"] = InfoMessages.PatientCreateSuccessMessage;
            return this.RedirectToAction("All");
        }

        // List Of Patients
        public IActionResult All()
        {
            var patientsViewModel = this.patientsService.GetAll<PatientViewModel>().ToList();
            return this.View(patientsViewModel);
        }

        // Delete Patient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var patient = await this.patientsService.GetById<PatientServiceModel>(id);
            var user = await this.userManager.FindByIdAsync(patient.UserId);
            if (patient == null || user == null)
            {
                return this.NotFound();
            }

            var result = await this.patientsService.Delete(patient);
            if (result == null)
            {
                await this.userManager.DeleteAsync(user);
            }
            else
            {
                user.IsDeleted = true;
                await this.userManager.UpdateAsync(user);
            }

            this.TempData["InfoMessage"] = InfoMessages.PatientDeleteSuccessMessage;
            return this.RedirectToAction("All");
            //var patient = await this.patientsService.GetById<PatientServiceModel>(id);
            //if (patient == null)
            //{
            //    return this.NotFound();
            //}

            //await this.patientsService.Delete(patient);
            //this.TempData["InfoMessage"] = InfoMessages.PatientDeleteSuccessMessage;
            //return this.RedirectToAction("All");
        }

        // Edit Patient
        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var patient = await this.patientsService.GetById<PatientServiceModel>(id);
            if (patient == null)
            {
                return this.NotFound();
            }

            var patientEditViewModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<PatientEditViewModel>(patient);
            return this.View(patientEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PatientEditViewModel patientEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(patientEditViewModel);
            }

            var url = this.patientsService.GetUrl(id);

            string photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                patientEditViewModel.Photo,
                patientEditViewModel.FirstName,
                GlobalConstants.CloudFolderForPatientsPhotos);

            var patientServiceModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<PatientServiceModel>(patientEditViewModel);
            if (patientEditViewModel.Photo == null)
            {
                patientServiceModel.Photo = url;
            }
            else
            {
                patientServiceModel.Photo = photoUrl;
            }

            var patientId = await this.patientsService.Edit(id, patientServiceModel);
            this.TempData["InfoMessage"] = InfoMessages.PatientEditSuccessMessage;
            return this.RedirectToAction("Details", "Patients", new { id = patientId });
        }

        // Patient Details
        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(string id)
        {
            var patient = await this.patientsService.GetById<PatientDetailsViewModel>(id);
            if (patient == null)
            {
                return this.NotFound();
            }

            return this.View(patient);
        }
    }
}
