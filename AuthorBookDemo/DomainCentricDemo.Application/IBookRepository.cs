using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Application;

public interface IBookRepository
{
    void Create(Book book);
    void Commit();
    Book Load(int id);
    void Save(Book book);
    void Delete(Book book);
}