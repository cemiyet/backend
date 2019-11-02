using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.EntityConfigurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
