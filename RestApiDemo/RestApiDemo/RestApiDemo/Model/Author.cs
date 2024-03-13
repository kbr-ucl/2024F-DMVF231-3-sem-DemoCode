using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.Model;

public class Author
{
    public int AuthorId { get; set; }
    [Required]
    public string Name { get; set; }
}