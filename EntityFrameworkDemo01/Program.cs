// See https://aka.ms/new-console-template for more information
using EntityFrameworkDemo01;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
db.SaveChanges();

// Read
Console.WriteLine("Querying for a blog");
var blog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
db.SaveChanges();

// Get all blogs
Console.WriteLine("All blogs with posts in database:");
var blogs = db.Blogs.Include(a => a.Posts);
foreach (var b in blogs)
{
    Console.WriteLine($"- {b.Url}");
    foreach (var p in b.Posts)
    {
        Console.WriteLine($"  - {p.Title}, {p.Content}");
    }
}
Console.WriteLine();

//// Delete
//Console.WriteLine("Delete the blog");
//db.Remove(blog);
//db.SaveChanges();