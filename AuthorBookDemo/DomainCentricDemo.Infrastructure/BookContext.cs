using DomainCentricDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace DomainCentricDemo.Infrastructure;

public class BookContext : DbContext
{
    private readonly bool _designTime;
    public BookContext()
    {
        _designTime = true;
    }

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_designTime)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=BookDb;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=true");
        }
    }

    public DbSet<Book> Books { get; set; } 
    public DbSet<Author> Authors { get; set; } 
}