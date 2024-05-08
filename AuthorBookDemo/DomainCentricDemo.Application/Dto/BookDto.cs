namespace DomainCentricDemo.Application.Dto;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<BookAuthorDto> Authors { get; set; }
}