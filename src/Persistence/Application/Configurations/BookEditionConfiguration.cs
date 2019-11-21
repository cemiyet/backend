using Cemiyet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cemiyet.Persistence.Application.Configurations
{
    public class BookEditionConfiguration : IEntityTypeConfiguration<BookEdition>
    {
        public void Configure(EntityTypeBuilder<BookEdition> builder)
        {
            builder.HasKey(be => be.Isbn);

            builder.Property(be => be.Isbn)
                   .HasMaxLength(13);

            builder.HasOne(be => be.Publisher)
                   .WithMany(p => p.BookEditions)
                   .HasForeignKey(be => be.PublishersId);

            builder.HasOne(be => be.Book)
                   .WithMany(b => b.Editions)
                   .HasForeignKey(be => be.BooksId);

            builder.HasOne(be => be.Dimensions)
                   .WithMany(d => d.BookEditions)
                   .HasForeignKey(be => be.DimensionsId);
        }
    }
}
