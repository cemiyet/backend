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
                new Genre { Name = "Anı", CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Genre { Name = "Anlatı", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Antoloji / Derleme", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Bilimkurgu", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Belgesel Roman", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Biyografi / Otobiyografi", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Çizgi Roman", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Deneme", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Destan", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Divan Edebiyatı", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Edebiyat Tarihi", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Edebiyat Yazıları", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Eleştiri", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Günlük", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Halk Edebiyatı", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Mizah", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Hikaye", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Söyleşi", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "İnceleme", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Mektup", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Polisiye", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Roman", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  },
                new Genre { Name = "Tarihi Roman", CreationDate = DateTime.Now, ModificationDate = DateTime.Now  }
            };

            context.Genres.AddRange(genres);
            context.SaveChanges();
        }

        private static void SeedDimensions(MainDataContext context)
        {
            if (context.Dimensions.Any()) return;

            var dimensions = new List<Dimension>
            {
                new Dimension { Width = 10.5, Height = 15.9, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 12.5, Height = 19.5, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 13, Height = 19.5, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 13.5, Height = 19.5, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 12.5, Height = 20.5, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 13.5, Height = 21, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 14.5, Height = 21.5, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 16, Height = 22, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 13.5, Height = 23, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 15.5, Height = 23, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 16, Height = 23.5, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 16.5, Height = 23.5, CreationDate = DateTime.Now, ModificationDate = DateTime.Now },
                new Dimension { Width = 16.5, Height = 26, CreationDate = DateTime.Now, ModificationDate = DateTime.Now }
            };

            context.Dimensions.AddRange(dimensions);
            context.SaveChanges();
        }
    }
}
