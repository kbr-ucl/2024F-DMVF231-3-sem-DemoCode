using AutoMapper;
using DomainCentricDemo.Application;
using DomainCentricDemo.Application.Dto;
using Microsoft.EntityFrameworkCore;

namespace DomainCentricDemo.Infrastructure.Queries;

public class BookQuery : IBookQuery
{
    private readonly BookContext _db;
    private readonly IMapper _mapper;

    public BookQuery(BookContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    BookDto? IBookQuery.Get(int id)
    {
        var book = _db.Books.AsNoTracking().Include(a => a.Authors).FirstOrDefault(a => a.Id == id);
        if (book == null) return null;

        return _mapper.Map<BookDto>(book);
    }

    List<BookDto> IBookQuery.GetAll()
    {
        return _mapper.Map<List<BookDto>>(_db.Books.Include(a => a.Authors).AsNoTracking());
    }
}