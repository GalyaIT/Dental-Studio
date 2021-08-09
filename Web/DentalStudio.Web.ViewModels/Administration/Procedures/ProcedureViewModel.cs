namespace DentalStudio.Web.ViewModels.Administration.Procedures
{
    using AutoMapper;

    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.Infrastructure;

    public class ProcedureViewModel : IMapFrom<ProcedureServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
             .CreateMap<ProcedureServiceModel, ProcedureViewModel>()
             .ForMember(
                destination => destination.Code,
                opts => opts.MapFrom(x => EnumExtensions.GetDisplayName(x.Code)));
        }
    }
}
