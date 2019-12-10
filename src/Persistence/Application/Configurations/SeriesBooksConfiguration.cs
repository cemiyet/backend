using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class SeriesBooksConfiguration : IEntityTypeConfiguration<SeriesBooks>
    {
        public void Configure(EntityTypeBuilder<SeriesBooks> builder)
        {
            builder.HasKey(sb => new { sb.SeriesId, sb.BooksId });

            builder.HasOne(sb => sb.Serie)
                   .WithMany(s => s.Books)
                   .HasForeignKey(sb => sb.SeriesId);

            builder.HasOne(sb => sb.Book)
                   .WithMany(b => b.Series)
                   .HasForeignKey(sb => sb.BooksId);
        }
    }
}
