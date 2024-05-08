using DomainCentricDemo.Application;
using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookContext _db;

    public AuthorRepository(BookContext db)
    {
        _db = db;
    }

    void IAuthorRepository.Commit()
    {
        _db.SaveChanges();
    }

    void IAuthorRepository.Create(Author book)
    {
        _db.Authors.Add(book);
    }

    void IAuthorRepository.Delete(Author book)
    {
        _db.Authors.Remove(book);
    }

    Author IAuthorRepository.Load(string id)
    {
        return _db.Authors.First(book => book.Id == id);
    }

    void IAuthorRepository.Save(Author book)
    {
    }
}