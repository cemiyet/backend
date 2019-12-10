using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class SerieConfiguration : IEntityTypeConfiguration<Serie>
    {
        public void Configure(EntityTypeBuilder<Serie> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Description).HasMaxLength(2000);
        }
    }
}
