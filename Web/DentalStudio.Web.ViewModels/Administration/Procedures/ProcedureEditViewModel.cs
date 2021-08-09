namespace DentalStudio.Web.ViewModels.Administration.Procedures
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using DentalStudio.Common;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class ProcedureEditViewModel : IMapTo<ProcedureServiceModel>, IMapFrom<ProcedureServiceModel>
    {
        [StringLength(Constraints.ProcedureMaxLength, ErrorMessage = ErrorMessages.StringLengthErrorMessage, MinimumLength = Constraints.ProcedureMinLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Процедура")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Display(Name = "Код")]
        public Code Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.RequiredErrorMessage)]
        [Range(typeof(decimal), Constraints.MinPrice, Constraints.MaxPrice, ErrorMessage = ErrorMessages.PriceErrorMessage)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
