namespace DentalStudio.Web.ViewComponents.Models
{
    using System.Collections.Generic;

    using DentalStudio.Web.ViewModels.About;

    public class DoctorAboutViewModel
    {
        public string FullName { get; set; }

        public string Specialty { get; set; }

        public string Photo { get; set; }

        public List<DoctorViewModel> Doctors { get; set; }
    }
}
