namespace EntityFrameworkDemo01;

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public List<Post> Posts { get; } = [];
    // public List<Author> Authors { get; } = []; 
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Blog Blog { get; set; }
}

//public class Author
//{
//    public int AuthorId { get; set; }
//    public string Name { get; set; }
//    public List<Post> Posts { get; } = []; 
//}