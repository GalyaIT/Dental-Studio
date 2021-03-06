namespace DentalStudio.Web.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;

    using DentalStudio.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/Identity/Account/Login");
        }
    }
}
