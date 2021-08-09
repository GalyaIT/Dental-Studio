namespace DentalStudio.Services.Data.Tests.Common
{
    using System.Reflection;

    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ProcedureServiceModel).GetTypeInfo().Assembly,
                typeof(Procedure).GetTypeInfo().Assembly);
        }
    }
}
