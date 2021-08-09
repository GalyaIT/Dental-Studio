namespace DentalStudio.Data.Models
{
    using System.Collections.Generic;

    using DentalStudio.Data.Common.Models;
    using DentalStudio.Data.Models.Enumerations;

    public class Procedure : BaseDeletableModel<int>, IAuditInfo, IDeletableEntity
    {
        public Procedure()
        {
            this.Appointments = new HashSet<Appointment>();
        }

        public string Name { get; set; }

        public Code Code { get; set; }

        public decimal Price { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
