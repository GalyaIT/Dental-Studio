using DentalStudio.Data.Models;
using DentalStudio.Data.Models.Enumerations;
using DentalStudio.Services.Mapping;

namespace DentalStudio.Services.Models
{
    public class ProcedureServiceModel : IMapTo<Procedure>, IMapFrom<Procedure>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Code Code { get; set; }

        public decimal Price { get; set; }
    }
}
