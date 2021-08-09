namespace DentalStudio.Web.Areas.Medicine.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DentalStudio.Common;
    using DentalStudio.Services;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Medicine.Doctors;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : MedicineController
    {

        private readonly IDoctorsService doctorsService;
        private readonly ICloudinaryService cloudinaryService;

        public DoctorsController(
            IDoctorsService doctorsService,
            ICloudinaryService cloudinaryService)
        {
            this.doctorsService = doctorsService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet(Name = "Profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = await this.doctorsService.GetDoctorById<DoctorProfileDetailsViewModel>(userId);
            return this.View(doctor);
        }

        // Edit DoctorProfile
        [HttpGet(Name = "EditProfile")]
        public async Task<IActionResult> EditProfile(string id)
        {
            var doctor = await this.doctorsService.GetById<DoctorServiceModel>(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            var doctorProfileEditViewModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<DoctorProfileEditViewModel>(doctor);
            return this.View(doctorProfileEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(string id, DoctorProfileEditViewModel doctorEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(doctorEditViewModel);
            }

            var url = this.doctorsService.GetUrl(doctorEditViewModel.Id);

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

            await this.doctorsService.Edit(id, doctorServiceModel);
            this.TempData["InfoMessage"] = InfoMessages.EditSuccessMessage;
            return this.RedirectToAction("Profile");
        }
    }
}
