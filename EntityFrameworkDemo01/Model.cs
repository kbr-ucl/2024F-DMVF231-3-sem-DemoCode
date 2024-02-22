namespace EntityFrameworkDemo01;

public class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public List<Post> Posts { get; } = [];
     public List<Author> Authors { get; } = []; 
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Blog Blog { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Blog> Blogs { get; } = [];
}