namespace DentalStudio.Data.Models
{

    using DentalStudio.Data.Common.Models;

    public class Medecine : BaseModel<int>
    {
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
