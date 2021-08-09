namespace DentalStudio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Models.Enumerations;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Administration.Appointments;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using DentalStudio.Web.ViewModels.Administration.Procedures;
    using DentalStudio.Web.ViewModels.Medicine.Appointments;
    using DentalStudio.Web.ViewModels.Medicine.Patients;
    using DentalStudio.Web.ViewModels.Medicine.Procedures;
    using DentalStudio.Web.ViewModels.Patients;
    using Microsoft.EntityFrameworkCore;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;
        private readonly IDoctorsService doctorsService;
        private readonly IPatientsService patientsService;
        private readonly IProceduresService proceduresService;
        private DateTime currentDate = DateTime.UtcNow.AddDays(-1);

        public AppointmentsService(
            IDeletableEntityRepository<Appointment> appointmentsRepository,
            IDoctorsService doctorsService,
            IPatientsService patientsService,
            IProceduresService proceduresService)
        {
            this.appointmentsRepository = appointmentsRepository;
            this.doctorsService = doctorsService;
            this.patientsService = patientsService;
            this.proceduresService = proceduresService;
        }

        public async Task<AppointmentServiceModel> Create(AppointmentCreateInputModel model)
        {
            var doctor = await this.doctorsService.GetByName<DoctorAppointmentViewModel>(model.DoctorName);
            var patient = await this.patientsService.GetByName<PatientAppointmentViewModel>(model.PatientName);
            var procedure = await this.proceduresService.GetByName<ProcedureAppointmentViewModel>(model.ProcedureName);

            var appointment = new Appointment();
            if (model.Date >= DateTime.Now.Date)
            {
                appointment.PatientId = patient.Id;
                appointment.DoctorId = doctor.Id;
                appointment.Date = model.Date;
                appointment.Time = model.Time;
                appointment.ProcedureId = procedure.Id;
                appointment.Status = Status.Waiting;

                var existedAppointment = this.appointmentsRepository.All().FirstOrDefault(a => a.Date == appointment.Date &&
               a.Time.TimeOfDay == appointment.Time.TimeOfDay && a.DoctorId == appointment.DoctorId);
                if (existedAppointment == null)
                {
                    await this.appointmentsRepository.AddAsync(appointment);
                    await this.appointmentsRepository.SaveChangesAsync();
                }
            }

            var appointmentDb = this.appointmentsRepository.All().FirstOrDefault(a => a.Id == appointment.Id);
            var appointmentModel = AutoMapperConfig.MapperInstance.Map<AppointmentServiceModel>(appointmentDb);
            return appointmentModel;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var query =
                 this.appointmentsRepository.All().Where(a => a.Patient.IsDeleted == false)
                 .To<AppointmentServiceModel>()
                 .Where(a => a.Date >= this.currentDate);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByDoctor<T>(string userId)
        {
            var query =
              this.appointmentsRepository.All().
              Where(a => a.Patient.IsDeleted == false)
              .To<AppointmentServiceModel>()
              .Where(a => a.Date >= this.currentDate && a.DoctorId == userId);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllWaitingByDoctor<T>(string userId)
        {
            var query =
              this.appointmentsRepository.All()
              .Where(a => a.Patient.IsDeleted == false)
              .To<AppointmentServiceModel>()
              .Where(a => a.Date >= this.currentDate && a.Status == Status.Waiting && a.DoctorId == userId);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllConfirmedByDoctor<T>(string userId)
        {
            var query =
              this.appointmentsRepository.All()
              .Where(a => a.Patient.IsDeleted == false)
              .To<AppointmentServiceModel>()
              .Where(a => a.Date >= this.currentDate && a.Status == Status.Confirmed && a.DoctorId == userId);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllRefusedByDoctor<T>(string userId)
        {
            var query =
               this.appointmentsRepository.All()
               .Where(a => a.Patient.IsDeleted == false)
               .To<AppointmentServiceModel>()
               .Where(a => a.Date >= this.currentDate && a.Status == Status.Refused && a.DoctorId == userId);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllPatientByDoctor<T>(string id)
        {
            var query =
              this.appointmentsRepository.All()
              .Where(a => a.Patient.IsDeleted == false)
              .To<AppointmentServiceModel>()
              .Where(a => a.DoctorId == id)
              .Select(a => a.Patient);

            return query.To<T>().Distinct().ToList();
        }

        public async Task<string> Edit(int id, AppointmentEditViewModel appointmentEditViewModel)
        {
            Appointment appointment = this.appointmentsRepository.All().FirstOrDefault(a => a.Id == id);
            var doctor = await this.doctorsService.GetByName<DoctorAppointmentViewModel>(appointmentEditViewModel.DoctorName);
            var patient = await this.patientsService.GetByName<PatientAppointmentViewModel>(appointmentEditViewModel.PatientName);
            var procedure = await this.proceduresService.GetByName<ProcedureAppointmentViewModel>(appointmentEditViewModel.ProcedureName);
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }

            if (appointment.Date >= DateTime.Now.Date)
            {
                appointment.PatientId = patient.Id;
                appointment.DoctorId = doctor.Id;
                appointment.Date = appointmentEditViewModel.Date;
                appointment.Time = appointmentEditViewModel.Time;
                appointment.ProcedureId = procedure.Id;
                appointment.Status = appointmentEditViewModel.Status;

                var count = this.appointmentsRepository.All().Where(a => a.Date == appointment.Date &&
                     a.Time.TimeOfDay == appointment.Time.TimeOfDay && a.DoctorId == appointment.DoctorId &&
                     a.PatientId != appointment.PatientId)
                     .Count();
                var existedAppointment = this.appointmentsRepository.All().FirstOrDefault(a => a.Date == appointment.Date &&
                  a.Time.TimeOfDay == appointment.Time.TimeOfDay && a.DoctorId == appointment.DoctorId && a.PatientId == appointment.PatientId);
                if (count == 0 && existedAppointment == null)
                {
                    appointment.ProcedureId = procedure.Id;
                    appointment.Status = appointmentEditViewModel.Status;
                    this.appointmentsRepository.Update(appointment);
                    await this.appointmentsRepository.SaveChangesAsync();
                }
                else if (existedAppointment != null)
                {
                    appointment.ProcedureId = procedure.Id;
                    appointment.Status = appointmentEditViewModel.Status;
                    this.appointmentsRepository.Update(appointment);
                    await this.appointmentsRepository.SaveChangesAsync();
                }
                else
                {
                    return null;
                }
            }

            var appointmentModel = AutoMapperConfig.MapperInstance.Map<AppointmentServiceModel>(appointment);
            return appointmentModel.Id.ToString();
        }

        public async Task<T> GetById<T>(int id)
        {
            var appointment = await this.appointmentsRepository
               .All()
               .Where(a => a.Id == id).To<AppointmentServiceModel>()
               .FirstOrDefaultAsync();

            var appointmentServiceModel = AutoMapperConfig.MapperInstance.Map<T>(appointment);

            return appointmentServiceModel;
        }

        public async Task<bool> Delete(AppointmentServiceModel appointmentServiceModel)
        {
            Appointment appointment = await this.appointmentsRepository.All().FirstOrDefaultAsync(a => a.Id == appointmentServiceModel.Id);

            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }

            this.appointmentsRepository.Delete(appointment);

            var result = await this.appointmentsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<AppointmentServiceModel> CreateByDoctor(DoctorAppointmentCreateModel model, string doctorId)
        {
            var patient = await this.patientsService.GetByName<PatientAppointmentViewModel>(model.PatientName);
            var procedure = await this.proceduresService.GetByName<ProcedureAppointmentViewModel>(model.ProcedureName);

            var appointment = new Appointment();
            if (model.Date >= DateTime.Now.Date)
            {
                appointment.PatientId = patient.Id;
                appointment.DoctorId = doctorId;
                appointment.Date = model.Date;
                appointment.Time = model.Time;
                appointment.ProcedureId = procedure.Id;
                appointment.Status = Status.Confirmed;

                var existedAppointment = this.appointmentsRepository.All().FirstOrDefault(a => a.Date == appointment.Date &&
                a.Time.TimeOfDay == appointment.Time.TimeOfDay && a.DoctorId == appointment.DoctorId);
                if (existedAppointment == null)
                {
                    await this.appointmentsRepository.AddAsync(appointment);
                    await this.appointmentsRepository.SaveChangesAsync();
                }
            }

            var appointmentDb = this.appointmentsRepository.All().FirstOrDefault(a => a.Id == appointment.Id);
            var appointmentModel = AutoMapperConfig.MapperInstance.Map<AppointmentServiceModel>(appointmentDb);
            return appointmentModel;
        }

        public async Task<string> EditByDoctor(int id, DoctorAppointmentEditViewModel appointmentEditViewModel, string doctorId)
        {
            Appointment appointment = this.appointmentsRepository.All().FirstOrDefault(a => a.Id == id);
            var patient = await this.patientsService.GetByName<PatientDoctorAppointmentViewModel>(appointmentEditViewModel.PatientName);
            var procedure = await this.proceduresService.GetByName<ProcedureDoctorAppointmentViewModel>(appointmentEditViewModel.ProcedureName);
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }

            if (appointment.Date >= DateTime.Now.Date)
            {
                appointment.DoctorId = doctorId;
                appointment.Date = appointmentEditViewModel.Date;
                appointment.Time = appointmentEditViewModel.Time;
                appointment.PatientId = patient.Id;
                var count = this.appointmentsRepository.All().Where(a => a.Date == appointment.Date &&
                      a.Time.TimeOfDay == appointment.Time.TimeOfDay && a.DoctorId == appointment.DoctorId &&
                      a.PatientId != appointment.PatientId)
                      .Count();
                var existedAppointment = this.appointmentsRepository.All().FirstOrDefault(a => a.Date == appointment.Date &&
                  a.Time.TimeOfDay == appointment.Time.TimeOfDay && a.DoctorId == appointment.DoctorId && a.PatientId == appointment.PatientId);
                if (count == 0 && existedAppointment == null)
                {
                    appointment.ProcedureId = procedure.Id;
                    appointment.Status = appointmentEditViewModel.Status;
                    this.appointmentsRepository.Update(appointment);
                    await this.appointmentsRepository.SaveChangesAsync();
                }
                else if (existedAppointment != null)
                {
                    appointment.ProcedureId = procedure.Id;
                    appointment.Status = appointmentEditViewModel.Status;
                    this.appointmentsRepository.Update(appointment);
                    await this.appointmentsRepository.SaveChangesAsync();
                }
                else
                {
                    return null;
                }
            }

            var appointmentModel = AutoMapperConfig.MapperInstance.Map<AppointmentServiceModel>(appointment);
            return appointmentModel.Id.ToString();
        }

        public IEnumerable<T> GetAllAppointmentsByPatient<T>(string patientId)
        {
            var query =
             this.appointmentsRepository.All().To<AppointmentServiceModel>()
             .Where(a => a.Date >= this.currentDate && a.PatientId == patientId)
             .OrderBy(a => a.Date).ThenBy(a => a.Time.TimeOfDay);

            return query.To<T>().ToList();
        }

        public async Task<AppointmentServiceModel> CreateByPatient(PatientAppointmentCreateModel model, string patientId)
        {
            var doctor = await this.doctorsService.GetByName<DoctorPatientAppointmentViewModel>(model.DoctorName);
            var procedure = await this.proceduresService.GetByName<ProcedureAppointmentViewModel>(model.ProcedureName);

            var appointment = new Appointment();
            if (model.Date >= DateTime.Now.Date)
            {
                appointment.PatientId = patientId;
                appointment.DoctorId = doctor.Id;
                appointment.Date = model.Date;
                appointment.Time = model.Time;
                appointment.ProcedureId = procedure.Id;
                appointment.Status = Status.Waiting;

                var existedAppointment = this.appointmentsRepository.All().FirstOrDefault(a => a.Date == appointment.Date &&
                a.Time.TimeOfDay == appointment.Time.TimeOfDay && a.DoctorId == appointment.DoctorId);
                if (existedAppointment == null)
                {
                    await this.appointmentsRepository.AddAsync(appointment);
                    await this.appointmentsRepository.SaveChangesAsync();
                }
            }

            var appointmentDb = this.appointmentsRepository.All().FirstOrDefault(a => a.Id == appointment.Id);
            var appointmentModel = AutoMapperConfig.MapperInstance.Map<AppointmentServiceModel>(appointmentDb);
            return appointmentModel;
        }

        public int GetCount()
        {
            return this.appointmentsRepository.All().Count();
        }

        public IEnumerable<T> GetAllAppointmentsByPatientAndDoctor<T>(string patientId, string doctorId)
        {
            var query =
            this.appointmentsRepository.All().To<AppointmentServiceModel>()
            .Where(a => a.Date >= this.currentDate && a.PatientId == patientId && a.DoctorId == doctorId)
            .OrderBy(a => a.Date).ThenBy(a => a.Time.TimeOfDay);

            return query.To<T>().ToList();
        }
    }
}
