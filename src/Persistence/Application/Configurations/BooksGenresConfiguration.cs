using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class BooksGenresConfiguration : IEntityTypeConfiguration<BooksGenres>
    {
        public void Configure(EntityTypeBuilder<BooksGenres> builder)
        {
            builder.HasKey(bg => new { bg.BooksId, bg.GenresId });

            builder.HasOne(bg => bg.Book)
                   .WithMany(b => b.Genres)
                   .HasForeignKey(bg => bg.BooksId);

            builder.HasOne(bg => bg.Genre)
                   .WithMany(g => g.Books)
                   .HasForeignKey(bg => bg.GenresId);
        }
    }
}
