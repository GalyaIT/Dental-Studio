namespace DentalStudio.Web.ViewComponents
{
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Data.Models;
    using DentalStudio.Services.Data;
    using DentalStudio.Web.ViewComponents.Models;
    using DentalStudio.Web.ViewModels.Medicine.Doctors;
    using DentalStudio.Web.ViewModels.Patients;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProfilePhotoViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDoctorsService doctorsService;
        private readonly IPatientsService patientsService;

        public ProfilePhotoViewComponent(
            UserManager<ApplicationUser> userManager,
            IDoctorsService doctorsService,
            IPatientsService patientsService)
        {
            this.userManager = userManager;
            this.doctorsService = doctorsService;
            this.patientsService = patientsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            var userId = user.Id;
            var model = new ProfilePhotoModel();
            if (this.User.IsInRole(GlobalConstants.DoctorRoleName))
            {
                DoctorProfilePhotoViewModel doctor = await this.doctorsService.GetDoctorById<DoctorProfilePhotoViewModel>(userId);
                model.FullName = doctor.FullName;
                model.ProfilePhoto = doctor.Photo;
            }
            else
            {
                PatientProfilePhotoViewModel patient = await this.patientsService.GetPatientById<PatientProfilePhotoViewModel>(userId);
                model.FullName = patient.FullName;
                if (patient.Photo == null)
                {
                    model.ProfilePhoto = GlobalConstants.DefaultPhoto;
                }
                else
                {
                    model.ProfilePhoto = patient.Photo;
                }
            }

            return this.View(model);
        }
    }
}
