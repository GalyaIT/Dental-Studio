namespace DentalStudio.Data.Configurations
{

    using DentalStudio.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> post)
        {
            post
                .HasKey(p => p.Id);
            post.Property(p => p.Title)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();
        }
    }
}
