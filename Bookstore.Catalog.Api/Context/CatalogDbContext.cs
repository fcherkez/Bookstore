using Bookstore.Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Catalog.Api.Context
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Language> Languages { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<Book>(b => 
            {
                //b.HasKey(x => x.BookID);
                b.HasIndex(x => x.ISBN).IsUnique();
                b.Property(x => x.Title).IsRequired().HasMaxLength(300);
                b.Property(x => x.Price).HasColumnType("decimal(10,2)");

                b.HasOne(x => x.Publisher).WithMany(x => x.Books).HasForeignKey(x => x.PublisherID);
                b.HasOne(x => x.Language).WithMany(x => x.Books).HasForeignKey(x => x.LanguageID);
                b.HasMany(x => x.Authors).WithOne(x => x.Book).HasForeignKey(x => x.BookID);
                b.HasMany(x => x.Genres).WithOne(x => x.Book).HasForeignKey(x => x.BookID);
            });

            mb.Entity<Author>(a =>
            {
                a.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                a.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                a.HasMany(x => x.Books).WithOne(x => x.Author).HasForeignKey(x => x.AuthorID);
            });

            mb.Entity<Publisher>(p =>
            {
                p.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
                
                p.HasMany(x => x.Books).WithOne(x => x.Publisher).HasForeignKey(x => x.PublisherID);
                p.HasData(new Publisher()
                {
                    PublisherID = 1,
                    CompanyName = "Test Publisher",
                    Country = "UK",
                    Website = "http://google.com"
                
              });
            });

            mb.Entity<Language>(l =>
            {
                l.Property(x => x.Name).IsRequired().HasMaxLength(100);

                l.HasMany(x => x.Books).WithOne(x => x.Language).HasForeignKey(x => x.LanguageID);
                l.HasData(new Language()
                {
                    LanguageID = 1,
                    Name = "Bulgarian"
                },
              new Language()
              {
                  LanguageID = 2,
                  Name = "English"
              });
            });

            mb.Entity<Genre>(g =>
            {
                g.Property(x => x.Name).IsRequired().HasMaxLength(100);

               g.HasMany(x => x.Books).WithOne(x => x.Genre).HasForeignKey(x => x.GenreID);
                g.HasData(new Genre()
                {
                    GenreID = 1,
                    Name = "Fantasy"
                },
             new Genre()
             {
                 GenreID = 2,
                 Name = "Technology"
             },
              new Genre()
              {
                  GenreID = 3,
                  Name = "Technology"
              },
              new Genre()
              {
                  GenreID = 4,
                  Name = "Comedy"
              });

            });

            mb.Entity<BookAuthor>(ba =>
            {
                ba.HasKey(x => new { x.BookID, x.AuthorID});
                
            });

            mb.Entity<BookGenre>(bg =>
            {
                bg.HasKey(x => new { x.BookID, x.GenreID });

            });

        }
    }
}
