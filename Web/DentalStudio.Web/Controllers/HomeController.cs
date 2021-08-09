namespace DentalStudio.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IPatientsService patientsService;

        public HomeController(
            IPatientsService patientsService)
        {
            this.patientsService = patientsService;
        }

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    return this.Redirect("/Administration/Dashboard/Index");
                }
                else if (this.User.IsInRole(GlobalConstants.DoctorRoleName))
                {
                    return this.Redirect("/Medicine/Dashboard/Index");
                }
                else if (this.User.IsInRole(GlobalConstants.PatientRoleName))
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var patient = await this.patientsService.GetPatientById<PatientServiceModel>(userId);

                    if (patient.Address == null || patient.PhoneNumber == null || patient.DateOfBirth == null)
                    {
                        return this.Redirect($"/Patients/UpdateProfile?id={patient.Id}&userId={userId}");
                    }
                    else
                    {
                        return this.Redirect($"/Patients/Index");
                    }

                }
            }

            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == StatusCodes.NotFound)
            {
                return this.Redirect($"/Error/{StatusCodes.NotFound}");
            }

            return this.Redirect($"/Error/{StatusCodes.InternalServerError}");
        }
    }
}
