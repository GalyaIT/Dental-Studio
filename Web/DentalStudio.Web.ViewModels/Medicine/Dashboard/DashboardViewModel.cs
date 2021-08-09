namespace DentalStudio.Web.ViewModels.Medicine.Dashboard
{
    using System.Collections.Generic;

    using DentalStudio.Web.ViewModels.Medicine.Appointments;
    using DentalStudio.Web.ViewModels.Medicine.Patients;

    public class DashboardViewModel
    {

        public List<PatientDoctorViewModel> Patients { get; set; }

        public List<AppointmentDoctorViewModel> WaitingAppointments { get; set; }

        public List<AppointmentDoctorViewModel> ConfirmedAppointments { get; set; }

        public List<AppointmentDoctorViewModel> AllAppointments { get; set; }
    }
}
