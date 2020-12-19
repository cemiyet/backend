using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class AuthorsBooksConfiguration : IEntityTypeConfiguration<AuthorsBooks>
    {
        public void Configure(EntityTypeBuilder<AuthorsBooks> builder)
        {
            builder.HasKey(ab => new { ab.AuthorsId, ab.BooksId });

            builder.HasOne(ab => ab.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(ab => ab.AuthorsId);

            builder.HasOne(ab => ab.Book)
                   .WithMany(b => b.Authors)
                   .HasForeignKey(ab => ab.BooksId);
        }
    }
}
