namespace DentalStudio.Data.Configurations
{
    using System;

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> appointment)
        {
            appointment
                .HasKey(ap => ap.Id);
            appointment
                .Property(ap => ap.Status)
                .IsRequired();
            appointment.HasOne(ap => ap.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(ap => ap.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            appointment.HasOne(ap => ap.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(ap => ap.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            appointment.HasOne(ap => ap.Procedure)
                .WithMany(pr => pr.Appointments)
                .HasForeignKey(ap => ap.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
