namespace DentalStudio.Data.Configurations
{

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ContactFormEntryConfiguration : IEntityTypeConfiguration<ContactFormEntry>
    {
        public void Configure(EntityTypeBuilder<ContactFormEntry> contact)
        {
            contact
                .HasKey(c => c.Id);
            contact
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();
            contact
                .Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired();
            contact
                .Property(c => c.Title)
                .HasMaxLength(100)
                .IsRequired();
            contact
               .Property(c => c.Content)
               .HasMaxLength(10000)
               .IsUnicode()
               .IsRequired();
        }
    }
}
