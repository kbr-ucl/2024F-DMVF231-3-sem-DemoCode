using AutoMapper;
using DomainCentricDemo.Web.Infrastructure;
using DomainCentricDemo.Web.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.Web.Pages.Book;

// https://learn.microsoft.com/en-us/aspnet/core/security/authorization/resourcebased?view=aspnetcore-7.0
[Authorize]
public class EditModel : PageModel
{
    private readonly IBookApiProxy _api;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public EditModel(IAuthorizationService authorizationService, IBookApiProxy api, IMapper mapper)
    {
        _authorizationService = authorizationService;
        _api = api;
        _mapper = mapper;
    }

    [BindProperty] public BookViewModel Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();


        var book = await _api.GetAsync(id.Value);
        if (book == null) return NotFound();

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, book, "IsSoleAuthorOrAdminPolicy");

        if (authorizationResult.Succeeded)
        {
            Book = _mapper.Map<BookViewModel>(book);
            return Page();
        }

        if (User.Identity is {IsAuthenticated: true}) return new ForbidResult();

        return new ChallengeResult();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        await _api.UpdateAsync(_mapper.Map<BookDto>(Book));
        return RedirectToPage("./Index");
    }
}