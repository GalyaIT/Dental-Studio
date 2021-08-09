namespace DentalStudio.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [Route("/Error/500")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult InternalServerError()
        {
            var errorViewModel = new ErrorViewModel
            {
                StatusCode = StatusCodes.InternalServerError,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };

            return this.View(errorViewModel);
        }

        [Route("/Error/404")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> NotFoundError()
        {
            var errorViewModel = new ErrorViewModel
            {
                StatusCode = StatusCodes.NotFound,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };

            return this.View(errorViewModel);
        }
    }
}
