namespace BookApi.Data.Migrations
{
    using BookApi.Data.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : System.Data.Entity.Migrations.DbMigrationsConfiguration<Data.AppDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.AppDataContext context)
        {
            IList<Book> initialBooks = new List<Book>();
            initialBooks.Add(new Data.Entity.Book()
            {
                BookId = "DC6C0E3B-5B3D-4AEF-B869-DC8BC3D977C3",
                Name = "Processing Big Data with Azure HDInsight",
                Authors = "Vinit Yadav",
                NumberOfPages = 207,
                DateOfPublication = new DateTime(2017, 06, 01),
                CreatedDate = DateTime.UtcNow
            });
            initialBooks.Add(new Data.Entity.Book()
            {
                BookId = "60BABF7C-2EA2-4802-B3F4-91C0DD952587",
                Name = "Develop Microsoft HoloLens Apps",
                Authors = "Allen G. Taylor",
                NumberOfPages = 300,
                DateOfPublication = new DateTime(2016, 02, 01),
                CreatedDate = DateTime.UtcNow
            });
            initialBooks.Add(new Data.Entity.Book()
            {
                BookId = "F1502160-221D-4114-A790-4960B8B8C03E",
                Name = "Business in Real-Time Using Azure IoT and Cortana Intelligence Suite",
                Authors = "Bob Familiar,Jeff Barnes",
                NumberOfPages = 175,
                DateOfPublication = new DateTime(2017, 03, 01),
                CreatedDate = DateTime.UtcNow
            });
            initialBooks.Add(new Data.Entity.Book()
            {
                BookId = "B67B379A-FA51-48AB-AC17-DB6F676A43A5",
                Name = "Building Microservices",
                Authors = "Sam Newman",
                NumberOfPages = 350,
                DateOfPublication = new DateTime(2015, 01, 01),
                CreatedDate = DateTime.UtcNow
            });

            foreach (Book book in initialBooks)
            {
                context.Books.AddOrUpdate(book);
            }

            base.Seed(context);
        }
    }
}
