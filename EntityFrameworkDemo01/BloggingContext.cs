using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemo01;

public class BloggingContext : DbContext
{
    public BloggingContext()
    {
        string folder = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.GetFullPath(Path.Combine(folder, @"..\..\..\"));
        DbPath = Path.Join(path, "blogging.db");
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Author> Authors { get; set; }

    public string DbPath { get; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}