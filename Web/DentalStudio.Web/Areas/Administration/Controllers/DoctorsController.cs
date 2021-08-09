namespace DentalStudio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Data.Models;
    using DentalStudio.Services;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Accounts;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDoctorsService doctorsService;
        private readonly ICloudinaryService cloudinaryService;

        public DoctorsController(
            UserManager<ApplicationUser> userManager,
            IDoctorsService doctorsService,
            ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.doctorsService = doctorsService;
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
                await this.userManager.AddToRoleAsync(user, GlobalConstants.DoctorRoleName);

                return this.RedirectToAction("AddDoctor", "Doctors", new { id = user.Id });
            }

            return this.NotFound();
        }

        // Add Doctor
        [HttpGet]
        public async Task<IActionResult> AddDoctor(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.NotFound();
            }

            var doctorCreateInputModel = new DoctorCreateInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserId = user.Id,
            };
            return this.View(doctorCreateInputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDoctor(string id, DoctorCreateInputModel doctorCreateModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(doctorCreateModel);
            }

            string photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                 doctorCreateModel.Photo,
                 doctorCreateModel.FirstName,
                 GlobalConstants.CloudFolderForDoctorsPhotos);

            var doctorServiceModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<DoctorServiceModel>(doctorCreateModel);
            doctorServiceModel.Photo = photoUrl;
            await this.doctorsService.AddDoctor(doctorServiceModel, id);
            this.TempData["InfoMessage"] = InfoMessages.DoctorCreateSuccessMessage;
            return this.RedirectToAction("All");
        }

        // List Of Doctors
        public IActionResult All()
        {
            var doctorsViewModel = this.doctorsService.GetAll<DoctorViewModel>().ToList();
            return this.View(doctorsViewModel);
        }

        // Delete Doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var doctor = await this.doctorsService.GetById<DoctorServiceModel>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            await this.doctorsService.Delete(doctor);
            this.TempData["InfoMessage"] = InfoMessages.DoctorDeleteSuccessMessage;
            return this.RedirectToAction("All");
        }

        // Edit Doctor
        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var doctor = await this.doctorsService.GetById<DoctorServiceModel>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            var doctorEditViewModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<DoctorEditViewModel>(doctor);
            return this.View(doctorEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DoctorEditViewModel doctorEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(doctorEditViewModel);
            }

            var url = this.doctorsService.GetUrl(id);
            string photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                doctorEditViewModel.Photo,
                doctorEditViewModel.FirstName,
                GlobalConstants.CloudFolderForDoctorsPhotos);
            var doctorServiceModel = AutoMapperConfig.MapperInstance.Map<DoctorServiceModel>(doctorEditViewModel);
            if (doctorEditViewModel.Photo == null)
            {
                doctorServiceModel.Photo = url;
            }
            else
            {
                doctorServiceModel.Photo = photoUrl;
            }

            var doctorId = await this.doctorsService.Edit(id, doctorServiceModel);
            this.TempData["InfoMessage"] = InfoMessages.DoctorEditSuccessMessage;
            return this.RedirectToAction("Details", "Doctors", new { id = doctorId });
        }

        // Doctor Details
        [HttpGet(Name = "Details")]
        public async Task<IActionResult> Details(string id)
        {
            var doctor = await this.doctorsService.GetById<DoctorDetailsViewModel>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            return this.View(doctor);
        }
    }
}
