using System.Runtime.InteropServices.ComTypes;
using DomainCentricDemo.Application;
using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookContext _db;

    public BookRepository(BookContext db)
    {
        _db = db;
    }

    void IBookRepository.Commit()
    {
        _db.SaveChanges();
    }

    void IBookRepository.Create(Book book)
    {
        _db.Books.Add(book);
    }

    void IBookRepository.Delete(Book book)
    {
        _db.Books.Remove(book);
    }

    Book IBookRepository.Load(int id)
    {
        return _db.Books.First(book => book.Id == id);
    }

    void IBookRepository.Save(Book book)
    {
    }
}