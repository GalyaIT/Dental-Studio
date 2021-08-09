namespace DentalStudio.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using DentalStudio.Web.ViewModels.Administration.Appointments;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using DentalStudio.Web.ViewModels.Administration.Procedures;

    public class DashboardViewModel
    {
        public List<DoctorViewModel> Doctors { get; set; }

        public List<PatientViewModel> Patients { get; set; }

        public List<ProcedureViewModel> Procedures { get; set; }

        public List<AppointmentViewModel> Appointments { get; set; }
    }
}
