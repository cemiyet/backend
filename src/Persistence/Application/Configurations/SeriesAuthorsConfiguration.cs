using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class SeriesAuthorsConfiguration : IEntityTypeConfiguration<SeriesAuthors>
    {
        public void Configure(EntityTypeBuilder<SeriesAuthors> builder)
        {
            builder.HasKey(sa => new { sa.SeriesId, sa.AuthorsId });

            builder.HasOne(sa => sa.Serie)
                   .WithMany(s => s.Authors)
                   .HasForeignKey(sa => sa.SeriesId);

            builder.HasOne(sa => sa.Author)
                   .WithMany(a => a.Series)
                   .HasForeignKey(sa => sa.AuthorsId);
        }
    }
}
