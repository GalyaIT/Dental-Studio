namespace DentalStudio.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using DentalStudio.Common;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Procedures;
    using Microsoft.AspNetCore.Mvc;

    public class ProceduresController : AdministrationController
    {
        private readonly IProceduresService proceduresService;

        public ProceduresController(IProceduresService proceduresService)
        {
            this.proceduresService = proceduresService;
        }

        public IActionResult All()
        {
            var procedureViewModel = this.proceduresService.GetAll<ProcedureViewModel>().ToList();
            return this.View(procedureViewModel);
        }

        // Create Procedure
        [HttpGet(Name = "Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcedureCreateInputModel procedureCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(procedureCreateInputModel);
            }

            var procedureServiceModel = AutoMapperConfig.MapperInstance.Map<ProcedureServiceModel>(procedureCreateInputModel);
            await this.proceduresService.Create(procedureServiceModel);
            this.TempData["InfoMessage"] = InfoMessages.ProcedureCreateSuccessMessage;
            return this.Redirect("All");
        }

        // Delete Procedure
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var procedure = await this.proceduresService.GetById<ProcedureServiceModel>(id);
            if (procedure == null)
            {
                return this.NotFound();
            }

            await this.proceduresService.Delete(procedure);
            this.TempData["InfoMessage"] = InfoMessages.ProcedureDeleteSuccessMessage;
            return this.RedirectToAction("All");
        }

        // Edit Procedure
        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var procedure = await this.proceduresService.GetById<ProcedureServiceModel>(id);
            if (procedure == null)
            {
                return this.NotFound();
            }

            var procedureEditViewModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<ProcedureEditViewModel>(procedure);

            return this.View(procedureEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProcedureEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var procedureServiceModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<ProcedureServiceModel>(model);
            var procedure = await this.proceduresService.Edit(id, procedureServiceModel);
            this.TempData["InfoMessage"] = InfoMessages.ProcedureEditSuccessMessage;
            return this.RedirectToAction("All");
        }
    }
}
