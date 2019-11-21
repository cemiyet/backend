using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.Contexts
{
    public static class AppDataContextSeed
    {
        public static void Seed(AppDataContext context)
        {
            SeedGenres(context);
            SeedDimensions(context);
            SeedAuthors(context);
            SeedPublishers(context);
            SeedBooks(context);
            SeedBookEditions(context);
        }

        private static void SeedGenres(AppDataContext context)
        {
            if (context.Genres.Any()) return;

            var genres = new List<Genre>
            {
                new Genre
                {
                    Name = "Anı", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Anlatı", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Antoloji / Derleme", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Bilimkurgu", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Belgesel Roman", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Biyografi / Otobiyografi", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Çizgi Roman", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Deneme", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Destan", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Divan Edebiyatı", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Edebiyat Tarihi", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Edebiyat Yazıları", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Eleştiri", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Günlük", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Halk Edebiyatı", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Mizah", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Hikaye", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Söyleşi", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "İnceleme", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Mektup", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Polisiye", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Roman", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Tarih", CreationDate = DateTime.UtcNow
                },
                new Genre
                {
                    Name = "Tarihi Roman", CreationDate = DateTime.UtcNow
                }
            };


            context.Genres.AddRange(genres);
            context.SaveChanges();
        }

        private static void SeedDimensions(AppDataContext context)
        {
            if (context.Dimensions.Any()) return;

            var dimensions = new List<Dimension>
            {
                new Dimension
                {
                    Width = 10.5, Height = 15.9, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 10.5, Height = 17, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 12.5, Height = 19.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13, Height = 19.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13.5, Height = 19.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 12.5, Height = 20.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13.5, Height = 21, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 14.5, Height = 21.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16, Height = 22, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 13.5, Height = 23, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 15.5, Height = 23, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16, Height = 23.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16.5, Height = 23.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 16.5, Height = 26, CreationDate = DateTime.UtcNow
                }
            };

            context.Dimensions.AddRange(dimensions);
            context.SaveChanges();
        }

        private static void SeedAuthors(AppDataContext context)
        {
            if (context.Authors.Any()) return;

            var authors = new List<Author>
            {
                new Author
                {
                    Name = "Ömer", Surname = "Seyfettin", Bio = "", CreationDate = DateTime.UtcNow
                },
                new Author
                {
                    Name = "Ziya", Surname = "Gökalp", Bio = "", CreationDate = DateTime.UtcNow
                },
                new Author
                {
                    Name = "İsmail", Surname = "Gaspıralı", Bio = "", CreationDate = DateTime.UtcNow
                },
                new Author
                {
                    Name = "Namık", Surname = "Kemal", Bio = "", CreationDate = DateTime.UtcNow
                },
                new Author
                {
                    Name = "Mustafa Kemal", Surname = "Atatürk", Bio = "", CreationDate = DateTime.UtcNow
                },
                new Author
                {
                    Name = "Nihal", Surname = "Atsız", Bio = "", CreationDate = DateTime.UtcNow
                },
                new Author
                {
                    Name = "Yusuf", Surname = "Akçura", Bio = "", CreationDate = DateTime.UtcNow
                }
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();
        }

        private static void SeedPublishers(AppDataContext context)
        {
            if (context.Publishers.Any()) return;

            var publishers = new List<Publisher>
            {
                new Publisher
                {
                    Name = "Ötüken Neşriyat", Description = "", CreationDate = DateTime.UtcNow
                },
                new Publisher
                {
                    Name = "Türkiye İş Bankası Kültür Yayınları", Description = "", CreationDate = DateTime.UtcNow
                },
                new Publisher
                {
                    Name = "Yapı Kredi Yayınları", Description = "", CreationDate = DateTime.UtcNow
                },
                new Publisher
                {
                    Name = "İthaki Yayınları", Description = "", CreationDate = DateTime.UtcNow
                },
                new Publisher
                {
                    Name = "Kronik Kitap", Description = "", CreationDate = DateTime.UtcNow
                }
            };

            context.Publishers.AddRange(publishers);
            context.SaveChanges();
        }

        private static void SeedBooks(AppDataContext context)
        {
            if (context.Books.Any()) return;

            var books = new List<Book>
            {
                new Book
                {
                    Title = "Nutuk",
                    Description = @"Ey Türk Gençliği!

                                    Birinci vazifen, Türk istiklâlini, Türk Cumhuriyetini, ilelebet, muhafaza ve müdafaa etmektir.
                                    Mevcudiyetinin ve istikbalinin yegâne temeli budur. Bu temel, senin, en kıymetli hazinendir.
                                    İstikbalde dahi, seni bu hazineden mahrum etmek isteyecek, dahilî ve haricî bedhahların olacaktır.
                                    Bir gün, İstiklâl ve Cumhuriyeti müdafaa mecburiyetine düşersen, vazifeye atılmak için, içinde bulunacağın vaziyetin imkân ve şerâitini düşünmeyeceksin!
                                    Bu imkân ve şerâit, çok nâmüsait bir mahiyette tezahür edebilir.
                                    İstiklâl ve Cumhuriyetine kastedecek düşmanlar, bütün dünyada emsali görülmemiş bir galibiyetin mümessili olabilirler.
                                    Cebren ve hile ile aziz vatanın, bütün kaleleri zaptedilmiş, bütün tersanelerine girilmiş, bütün orduları dağıtılmış ve memleketin her köşesi bilfiil işgal edilmiş olabilir.
                                    Bütün bu şerâitten daha elîm ve daha vahim olmak üzere, memleketin dahilinde, iktidara sahip olanlar gaflet ve dalâlet ve hattâ hıyanet içinde bulunabilirler.
                                    Hattâ bu iktidar sahipleri şahsî menfaatlerini, müstevlilerin siyasi emelleriyle tevhit edebilirler.
                                    Millet, fakr ü zaruret içinde harap ve bîtap düşmüş olabilir.

                                    Ey Türk istikbalinin evlâdı! İşte, bu ahval ve şerâit içinde dahi, vazifen;
                                    Türk İstiklâl ve Cumhuriyetini kurtarmaktır!
                                    Muhtaç olduğun kudret, damarlarındaki asil kanda mevcuttur!",
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.Single(a => a.Name == "Mustafa Kemal" && a.Surname == "Atatürk")
                        }
                    },
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.Single(g => g.Name == "Anı")
                        },
                        new BooksGenres
                        {
                            Genre = context.Genres.Single(g => g.Name == "Anlatı")
                        },
                        new BooksGenres
                        {
                            Genre = context.Genres.Single(g => g.Name == "Tarih")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }

        private static void SeedBookEditions(AppDataContext context)
        {
            if (context.BookEditions.Any()) return;

            var bookEditions = new List<BookEdition>
            {
                new BookEdition
                {
                    Isbn = "9789944888349",
                    PageCount = 599,
                    PrintDate = new DateTime(2019, 6, 28),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.Single(p => p.Name == "Türkiye İş Bankası Kültür Yayınları"),
                    Book = context.Books.Single(b => b.Title == "Nutuk"),
                    Dimensions = context.Dimensions.Single(d => d.Width == 15.5 && d.Height == 23)
                },
                new BookEdition
                {
                    Isbn = "9789750820038",
                    PageCount = 1197,
                    PrintDate = new DateTime(2019, 11, 05),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.Single(p => p.Name == "Yapı Kredi Yayınları"),
                    Book = context.Books.Single(b => b.Title == "Nutuk"),
                    Dimensions = context.Dimensions.Single(d => d.Width == 10.5 && d.Height == 17)
                },
            };

            context.BookEditions.AddRange(bookEditions);
            context.SaveChanges();
        }
    }
}
