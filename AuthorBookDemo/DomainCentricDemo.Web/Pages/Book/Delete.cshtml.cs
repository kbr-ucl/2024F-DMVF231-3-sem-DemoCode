using AutoMapper;
using DomainCentricDemo.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.Web.Pages.Book;

public class DeleteModel : PageModel
{
    private readonly IBookApiProxy _bookApi;
    private readonly IMapper _mapper;

    public DeleteModel(IBookApiProxy bookApi, IMapper mapper)
    {
        _bookApi = bookApi;
        _mapper = mapper;
    }

    [BindProperty] public BookViewModel Book { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null) return NotFound();

        var book = await _bookApi.GetAsync(id.Value);
        if (book == null) return NotFound();

        Book = _mapper.Map<BookViewModel>(book);

        return Page();
    }

    public async Task<IActionResult> OnPost(int? id)
    {
        if (id == null) return NotFound();

        await _bookApi.DeleteAsync(id.Value);

        return RedirectToPage("./Index");
    }
}