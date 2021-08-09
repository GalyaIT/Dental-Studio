namespace DentalStudio.Data.Configurations
{
    using System;

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> doctor)
        {
            doctor
                .HasKey(d => d.Id);
            doctor
                .Property(d => d.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            doctor
                .Property(d => d.LastName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            doctor
               .Property(d => d.FullName)
               .HasMaxLength(100)
               .IsUnicode()
               .IsRequired();
            doctor
                .Property(d => d.Email)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();
            doctor
                .Property(d => d.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
            doctor
                .Property(d => d.Gender)
                .IsRequired();
            doctor
                .Property(d => d.Specialty)
                .HasMaxLength(30)
                .IsUnicode()
                .IsRequired();
            doctor
                .Property(d => d.Address)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();
            doctor
                .Property(d => d.Grade)
                .HasMaxLength(2000)
                .IsUnicode();
        }
    }
}
