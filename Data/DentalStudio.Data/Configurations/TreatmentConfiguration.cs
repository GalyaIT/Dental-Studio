namespace DentalStudio.Data.Configurations
{
    using System;

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> treatment)
        {
            treatment
                .HasKey(t => t.Id);
            treatment.Property(t => t.Description)
                .HasMaxLength(1000)
                .IsUnicode()
                .IsRequired();
            treatment.HasOne(t => t.Patient)
                .WithMany(p => p.Treatments)
                .HasForeignKey(t => t.PatientId);
            treatment.HasOne(t => t.Doctor)
               .WithMany(d => d.Treatments)
               .HasForeignKey(t => t.DoctorId);
        }
    }
}
