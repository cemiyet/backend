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
            SeedSeries(context);
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
                    Name = "Fantastik", CreationDate = DateTime.UtcNow
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
                    Width = 12, Height = 19.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 12.5, Height = 19.5, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 12.5, Height = 20.5, CreationDate = DateTime.UtcNow
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
                    Width = 13.5, Height = 21, CreationDate = DateTime.UtcNow
                },
                new Dimension
                {
                    Width = 14, Height = 23, CreationDate = DateTime.UtcNow
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
                },
                new Author
                {
                    Name = "Patrick", Surname = "Rothfuss", Bio = "", CreationDate = DateTime.UtcNow
                },
                new Author
                {
                    Name = "Frank", Surname = "Herbert", Bio = "", CreationDate = DateTime.UtcNow
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
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Anı")
                        },
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Anlatı")
                        },
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Tarih")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Mustafa Kemal" && a.Surname == "Atatürk")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Bozkurtlar",
                    Description = @"BOZKURTLAR, Ateş çocuk dergisinin 7 Ocak 1937’de çıkan 7. sayısından, 29 ve 30. sayılar haricinde, 40. sayısına kadar tefrika edilip kitap olarak yayınlanacağı 1946’ya dek yarım kalan Bozkurtların Ölümü ve onun devamı olarak 1949’da yayınlanan Bozkurtlar Diriliyor’un, Ötüken Neşriyat tarafından 1973’te büyük yazarının lütufkâr müsaadeleriyle birleştirilip neşredilen ilk baskısında aldığı yeni ismidir.
                                    Birinci Gök Türk Kağanlığı’nın çöküşü ve Kür Şad önderliğindeki 40 Türk bahadırının canları pahasına esarete başkaldırarak bağımsızlık ateşini yakmalarının, onların unutulmaz ihtilal girişiminden elli yıl sonra, Kutluk Şad liderliğindeki Türklerin İkinci Gök Türk Kağanlığı’nın kurt başlı sancağını yeniden yükseltmelerinin temiz ve ince işlenmiş destansı bir dille anlatıldığı bu büyük Türk romanı, yazarı hayattayken klasikleşmiş ve pek çok nesli millî gurur ve şuur yoluna sevk ederek ölümsüzleşmiştir.
                                    Bozkurtlar, okuyucularını asırlar öncesine, ata yurtlarını yoğuran eski tasa ve kıvançların, zafer ve yenilgilerin; bozkırda şekillenen eski Türk yaşayış ve töresinin, ahlâk ve erdemlerinin yüceltildiği ülküleştirilmiş bir kahramanlık diyarına taşır. Bu iklimin havasını soluyacak Türk çocukları, karşılarında, atalarının göz alıcı bir aydınlıkta parıldayan faziletli hayat sahnelerini bulacaklardır.",
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Tarihi Roman")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Nihal" && a.Surname == "Atsız")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Rüzgarın Adı",
                    Description = @"Benim Adım Kvothe
                                    Fantastik kurgu edebiyatının eşsiz bir masalı, bir kahramanın kendi ağzıyla anlattığı öyküsü işte böyle başlıyor. Bir keder öyküsü bu… bir kurtuluş öyküsü… bir adamın evrenin anlamını arayışının ve gerek o arayışın gerekse de onu sürdürmesini sağlayan gem vurulamaz iradenin bir efsaneye dönüşmesinin öyküsü…",
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Fantastik")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Patrick" && a.Surname == "Rothfuss")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Bilge Adamın Korkusu",
                    Description = @"“Her bilge adamın korktuğu üç şey vardır: fırtınalı bir deniz, aysız bir gece ve yumuşak başlı birinin öfkesi.”
                                    Bilge Adamın Korkusu’nda Kvothe kahramanlık yolundaki ilk adımlarını atıyor ve kendi ömrü dahilinde efsane haline gelmenin hayatı bir adam için ne kadar zor kılabileceğini öğreniyor.
                                    Uyuyan höyük krallarından prensesler kaçırdım. Trebon kasabasını yakıp kül ettim. Felurian’la bir gece geçirdim ve hem canıma hem de aklıma mukayyet olabildim. Çoğu insanın alındığından daha küçük bir yaşta Üniversite’den atıldım. Başkalarının gündüz gözüyle ağızlarına almaktan bile korktukları yollardan ay ışığı altında geçtim. Tanrılarla konuştum, kadınlar sevdim ve ozanları ağlatan şarkılar yazdım. Benim adım Kvothe. Belki beni duymuşsunuzdur.",
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Fantastik")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Patrick" && a.Surname == "Rothfuss")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Dune",
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Bilimkurgu")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Frank" && a.Surname == "Herbert")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Dune Mesihi",
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Bilimkurgu")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Frank" && a.Surname == "Herbert")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Dune Çocukları",
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Bilimkurgu")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Frank" && a.Surname == "Herbert")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Dune Tanrı İmparatoru",
                    Genres = new List<BooksGenres>
                    {
                        new BooksGenres
                        {
                            Genre = context.Genres.SingleOrDefault(g => g.Name == "Bilimkurgu")
                        }
                    },
                    Authors = new List<AuthorsBooks>
                    {
                        new AuthorsBooks
                        {
                            Author = context.Authors.SingleOrDefault(a => a.Name == "Frank" && a.Surname == "Herbert")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                }
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
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "Türkiye İş Bankası Kültür Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Nutuk"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 15.5 && d.Height == 23)
                },
                new BookEdition
                {
                    Isbn = "9789750820038",
                    PageCount = 1197,
                    PrintDate = new DateTime(2019, 11, 05),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "Yapı Kredi Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Nutuk"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 10.5 && d.Height == 17)
                },
                new BookEdition
                {
                    Isbn = "9789754378009",
                    PageCount = 584,
                    PrintDate = new DateTime(2019, 09, 01),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "Ötüken Neşriyat"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Bozkurtlar"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 12 && d.Height == 19.5)
                },
                new BookEdition
                {
                    Isbn = "9786053751342",
                    PageCount = 736,
                    PrintDate = new DateTime(2019, 08, 21),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "İthaki Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Rüzgarın Adı"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 14 && d.Height == 23)
                },
                new BookEdition
                {
                    Isbn = "9786053751595",
                    PageCount = 1144,
                    PrintDate = new DateTime(2019, 05, 30),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "İthaki Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Bilge Adamın Korkusu"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 14 && d.Height == 23)
                },
                new BookEdition
                {
                    Isbn = "9786053754794",
                    PageCount = 712,
                    PrintDate = new DateTime(2019, 09, 26),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "İthaki Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Dune"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 13.5 && d.Height == 21)
                },
                new BookEdition
                {
                    Isbn = "9786053755319",
                    PageCount = 304,
                    PrintDate = new DateTime(2019, 03, 14),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "İthaki Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Dune Mesihi"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 13.5 && d.Height == 21)
                },
                new BookEdition
                {
                    Isbn = "9786053756026",
                    PageCount = 552,
                    PrintDate = new DateTime(2019, 02, 14),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "İthaki Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Dune Çocukları"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 13.5 && d.Height == 21)
                },
                new BookEdition
                {
                    Isbn = "9786053757207",
                    PageCount = 512,
                    PrintDate = new DateTime(2019, 03, 14),
                    CreationDate = DateTime.UtcNow,
                    Publisher = context.Publishers.SingleOrDefault(p => p.Name == "İthaki Yayınları"),
                    Book = context.Books.SingleOrDefault(b => b.Title == "Dune Tanrı İmparatoru"),
                    Dimensions = context.Dimensions.SingleOrDefault(d => d.Width == 13.5 && d.Height == 21)
                }
            };

            context.BookEditions.AddRange(bookEditions);
            context.SaveChanges();
        }

        private static void SeedSeries(AppDataContext context)
        {
            if (context.Series.Any()) return;

            var series = new List<Serie>
            {
                new Serie
                {
                    Title = "Kralkatili Güncesi",
                    Books = new List<SeriesBooks>
                    {
                        new SeriesBooks
                        {
                            Order = 1,
                            Book = context.Books.SingleOrDefault(b => b.Title == "Rüzgarın Adı")
                        },
                        new SeriesBooks
                        {
                            Order = 2,
                            Book = context.Books.SingleOrDefault(b => b.Title == "Bilge Adamın Korkusu")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                },
                new Serie
                {
                    Title = "Dune",
                    Books = new List<SeriesBooks>
                    {
                        new SeriesBooks
                        {
                            Order = 1,
                            Book = context.Books.SingleOrDefault(b => b.Title == "Dune")
                        },
                        new SeriesBooks
                        {
                            Order = 2,
                            Book = context.Books.SingleOrDefault(b => b.Title == "Dune Mesihi")
                        },
                        new SeriesBooks
                        {
                            Order = 3,
                            Book = context.Books.SingleOrDefault(b => b.Title == "Dune Çocukları")
                        },
                        new SeriesBooks
                        {
                            Order = 4,
                            Book = context.Books.SingleOrDefault(b => b.Title == "Dune Tanrı İmparatoru")
                        }
                    },
                    CreationDate = DateTime.UtcNow
                }
            };

            context.Series.AddRange(series);
            context.SaveChanges();
        }
    }
}
