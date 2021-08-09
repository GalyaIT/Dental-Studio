namespace DentalStudio.Data.Configurations
{

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> procedure)
        {
            procedure
                .HasKey(pr => pr.Id);
            procedure
                .Property(pr => pr.Name)
                .HasMaxLength(300)
                .IsUnicode()
                .IsRequired();
            procedure
                .Property(pr => pr.Price)
                .HasColumnType("decimal(16, 2)");
        }
    }
}
