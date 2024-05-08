using DomainCentricDemo.Domain;
using DomainCentricDemo.Infrastructure;

namespace DomainCentricDemo.Api.Data.DataInitializer;

public static class AuthorAndBookDataInitializer
{
    public static void SeedData(BookContext db)
    {
        if (db.Books.Any()) return;

        var author1 = new Author {Id = "author@ucl.dk", Description = "Author 1", Title = "Author 1"};
        var author2 = new Author {Id = "kbr@ucl.dk", Description = "Author 2", Title = "Author 2"};
        var book1 = new Book {Description = "Book by Author 1", Title = "Book by Author 1"};
        var book2 = new Book {Description = "Book by Author 2", Title = "Book by Author 2"};
        var book3 = new Book {Description = "Book by Author 1 and 2", Title = "Book by Author 1 and 2"};

        author1.Books.Add(book1);
        author1.Books.Add(book3);
        author2.Books.Add(book2);
        author2.Books.Add(book3);

        db.Authors.Add(author1);
        db.Authors.Add(author2);

        db.SaveChanges();
    }
}