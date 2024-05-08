using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Application;

public interface IAuthorRepository
{
    void Create(Author book);
    void Commit();
    Author Load(string id);
    void Save(Author book);
    void Delete(Author book);
}