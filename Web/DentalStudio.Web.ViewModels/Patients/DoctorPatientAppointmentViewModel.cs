namespace DentalStudio.Web.ViewModels.Patients
{
    using AutoMapper;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;

    public class DoctorPatientAppointmentViewModel : IMapFrom<DoctorServiceModel>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration
        //    .CreateMap<DoctorServiceModel, DoctorPatientAppointmentViewModel>()
        //    .ForMember(
        //       destination => destination.FullName,
        //       opts => opts.MapFrom(x => x.FirstName + " " + x.LastName));
        //}
    }
}
