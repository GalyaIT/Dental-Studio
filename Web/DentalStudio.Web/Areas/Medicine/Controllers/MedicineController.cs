namespace DentalStudio.Web.Areas.Medicine.Controllers
{
    using DentalStudio.Common;
    using DentalStudio.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.DoctorRoleName)]
    [Area("Medicine")]
    public class MedicineController : BaseController
    {
    }
}
