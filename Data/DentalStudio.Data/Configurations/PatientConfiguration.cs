namespace DentalStudio.Data.Configurations
{
    using System;

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> patient)
        {
            patient
               .HasKey(p => p.Id);
            patient
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            patient
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            patient
               .Property(p => p.FullName)
               .HasMaxLength(100)
               .IsUnicode()
               .IsRequired();
            patient
                .Property(p => p.Email)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();
            patient
                .Property(p => p.PhoneNumber)
                .HasMaxLength(20);
            patient
                .Property(p => p.Gender);
            patient
                .Property(p => p.Address)
                .HasMaxLength(100)
                .IsUnicode();
        }
    }
}
