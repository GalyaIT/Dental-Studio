namespace DentalStudio.Web.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Services.Data;
    using DentalStudio.Web.ViewComponents.Models;
    using DentalStudio.Web.ViewModels.About;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsViewComponent : ViewComponent
    {
        private readonly IDoctorsService doctorsService;

        public DoctorsViewComponent(IDoctorsService doctorsService)
        {
            this.doctorsService = doctorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var doctors = this.doctorsService.GetAll<DoctorViewModel>();
            var headOfTheTeam = doctors.FirstOrDefault(d => d.FullName == GlobalConstants.HeadOfTheTeam);
            var model = new DoctorAboutViewModel
            {
                FullName = headOfTheTeam.FullName,
                Photo = headOfTheTeam.Photo,
                Specialty = headOfTheTeam.Specialty,
                Doctors = doctors.ToList(),
            };
            return this.View(model);
        }
    }
}
