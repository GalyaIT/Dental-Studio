namespace DentalStudio.Data.Configurations
{
    using System;

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MedecineConfiguration : IEntityTypeConfiguration<Medecine>
    {
        public void Configure(EntityTypeBuilder<Medecine> medecine)
        {
            medecine
                .HasKey(m => m.Id);
            medecine
                .Property(m => m.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            medecine
                .Property(m => m.Quantity)
                .IsRequired();
        }
    }
}
