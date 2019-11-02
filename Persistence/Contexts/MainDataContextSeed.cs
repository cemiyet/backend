using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Contexts
{
    public static class MainDataContextSeed
    {
        public static void Seed(MainDataContext context)
        {
            SeedGenres(context);
            SeedDimensions(context);
        }

        private static void SeedGenres(MainDataContext context)
        {
            if (context.Genres.Any()) return;
            var genres = new List<Genre>
            {
                new Genre
                {
                    Name = "Anı", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Anlatı", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Antoloji / Derleme", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Bilimkurgu", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Belgesel Roman", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Biyografi / Otobiyografi", CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Çizgi Roman", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Deneme", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Destan", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Divan Edebiyatı", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Edebiyat Tarihi", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Edebiyat Yazıları", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Eleştiri", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Günlük", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Halk Edebiyatı", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Mizah", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Hikaye", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Söyleşi", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "İnceleme", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Mektup", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Polisiye", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Roman", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Tarihi Roman", CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                }
            };


            context.Genres.AddRange(genres);
            context.SaveChanges();
        }

        private static void SeedDimensions(MainDataContext context)
        {
            if (context.Dimensions.Any()) return;

            var dimensions = new List<Dimension>
            {
                new Dimension
                {
                    Width = 10.5, Height = 15.9, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 12.5, Height = 19.5, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13, Height = 19.5, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13.5, Height = 19.5, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 12.5, Height = 20.5, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13.5, Height = 21, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 14.5, Height = 21.5, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16, Height = 22, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13.5, Height = 23, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 15.5, Height = 23, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16, Height = 23.5, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16.5, Height = 23.5, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16.5, Height = 26, CreationDate = DateTime.UtcNow, ModificationDate = DateTime.UtcNow
                }
            };

            context.Dimensions.AddRange(dimensions);
            context.SaveChanges();
        }
    }
}
