using AutoMapper;
using DomainCentricDemo.Web.Infrastructure;
using DomainCentricDemo.Web.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.Web.Pages.Book;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IBookApiProxy _api;
    private readonly IMapper _mapper;

    public CreateModel(IBookApiProxy api, IMapper mapper)
    {
        _api = api;
        _mapper = mapper;
    }

    [BindProperty] public BookViewModel Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        await _api.CreateAsync(_mapper.Map<BookDto>(Book));

        return RedirectToPage("./Index");
    }
}