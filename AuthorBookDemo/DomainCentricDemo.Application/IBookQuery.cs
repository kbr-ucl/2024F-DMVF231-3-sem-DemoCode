using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.Application;

public interface IBookQuery
{
    BookDto? Get(int id);
    List<BookDto> GetAll();
}