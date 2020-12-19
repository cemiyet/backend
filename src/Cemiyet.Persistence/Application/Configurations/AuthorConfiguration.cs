using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(25);

            builder.Property(a => a.Surname)
                   .IsRequired()
                   .HasMaxLength(25);

            builder.Property(a => a.Bio).HasMaxLength(2000);
        }
    }
}
