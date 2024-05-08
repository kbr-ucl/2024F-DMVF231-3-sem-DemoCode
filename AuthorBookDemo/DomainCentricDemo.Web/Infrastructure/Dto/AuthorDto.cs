namespace DomainCentricDemo.Web.Infrastructure.Dto;

public class AuthorDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<BookDto> Books { get; set; }
}