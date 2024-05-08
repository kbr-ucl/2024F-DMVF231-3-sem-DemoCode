using AutoMapper;
using DomainCentricDemo.Web.Infrastructure;
using DomainCentricDemo.Web.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainCentricDemo.Web.Pages.Book;

public class IndexModel : PageModel
{
    private readonly IBookApiProxy _bookApi;
    private readonly IMapper _mapper;
    private readonly IWetherApiProxy _wether;


    public IndexModel(IBookApiProxy bookApi, IMapper mapper, IWetherApiProxy wether)
    {
        _bookApi = bookApi;
        _mapper = mapper;
        _wether = wether;
    }

    public IList<BookViewModel> Books { get; set; } = default!;
    public IEnumerable<WetherDto> Weathers { get; set; }

    public async Task OnGet()
    {
        var books = await _bookApi.GetAllAsync();
        Books = _mapper.Map<List<BookViewModel>>(books);
        Weathers = await _wether.GetAllAsync() ?? new List<WetherDto>();
    }
}