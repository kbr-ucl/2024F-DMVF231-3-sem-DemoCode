using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiDemo.DTOs;
using RestApiDemo.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace RestApiDemo.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private static readonly Expression<Func<Book, BookDto>> AsBookDto =
        x => new BookDto
        {
            Title = x.Title,
            Author = x.Author.Name,
            Genre = x.Genre
        };

    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        return await _context.Books.Include(a => a.Author).Select(AsBookDto).ToListAsync();
    }

    // GET: api/Books/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await _context.Books.Include(a => a.Author).Where(a => a.BookId == id).Select(AsBookDto)
            .FirstOrDefaultAsync();

        if (book == null) return NotFound();

        return book;
    }

    [HttpGet("{id:int}/details")]
    public async Task<ActionResult<BookDetailDto>> GetBookDetail(int id)
    {
        var book = await (from b in _context.Books.Include(b => b.Author)
            where b.BookId == id
            select new BookDetailDto
            {
                Title = b.Title,
                Genre = b.Genre,
                PublishDate = b.PublishDate,
                Price = b.Price,
                Description = b.Description,
                Author = b.Author.Name
            }).FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpGet("{genre}")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByGenre(string genre)
    {
        var books = await _context.Books.Include(b => b.Author).Select(AsBookDto).ToListAsync();
        return books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    [HttpGet("/api/authors/{authorId:int}/books")]
    public async Task<IEnumerable<BookDto>> GetBooksByAuthor(int authorId)
    {
        return await _context.Books.Include(b => b.Author)
            .Where(b => b.AuthorId == authorId)
            .Select(AsBookDto).ToListAsync();
    }

    //[HttpGet("date/{pubdate:datetime}")]
    [HttpGet("date/{pubdate:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}")]
    // [HttpGet("date/{*pubdate:datetime:regex(\\d{{4}}/\\d{{2}}/\\d{{2}})}")] 
    public async Task<IEnumerable<BookDto>> GetBooks(DateTime pubdate)
    {
        return await _context.Books.Include(b => b.Author)
            .Where(b => b.PublishDate >= pubdate.Date && b.PublishDate < pubdate.Date.AddDays(1))
            .Select(AsBookDto).ToListAsync();
    }

}