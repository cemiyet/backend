using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.EntityConfigurations
{
    public class DimensionConfiguration : IEntityTypeConfiguration<Dimension>
    {
        public void Configure(EntityTypeBuilder<Dimension> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Width)
                .IsRequired()
                .HasColumnType("numeric(4,2)");

            builder.Property(d => d.Height)
                .IsRequired()
                .HasColumnType("numeric(4,2)");
        }
    }
}
