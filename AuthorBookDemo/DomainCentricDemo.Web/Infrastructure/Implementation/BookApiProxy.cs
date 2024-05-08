using DomainCentricDemo.Web.Infrastructure.Dto;
using Elfie.Serialization;

namespace DomainCentricDemo.Web.Infrastructure.Implementation;

public class BookApiProxy : IBookApiProxy
{
    private readonly HttpClient _client;
    private readonly string _ressource = "api/Book";

    public BookApiProxy(HttpClient client)
    {
        _client = client;
    }

    async Task IBookApiProxy.CreateAsync(BookDto book)
    {
        await _client.PostAsJsonAsync(_ressource, book);
    }

    async Task IBookApiProxy.DeleteAsync(int id)
    {
        await _client.DeleteAsync($"{_ressource}/{id}");
    }

    async Task<IEnumerable<BookDto>?> IBookApiProxy.GetAllAsync()
    {
        return await _client.GetFromJsonAsync<List<BookDto>>(_ressource);
    }

    async Task<BookDto?> IBookApiProxy.GetAsync(int id)
    {
        return await _client.GetFromJsonAsync<BookDto>($"{_ressource}/{id}");
    }

    async Task IBookApiProxy.UpdateAsync(BookDto book)
    {
        await _client.PutAsJsonAsync(_ressource, book);
    }
}

public class WetherApiProxy : IWetherApiProxy
{
    private readonly HttpClient _client;

    public WetherApiProxy(HttpClient client)
    {
        _client = client;
    }
    async Task<IEnumerable<WetherDto>?> IWetherApiProxy.GetAllAsync()
    {
        return await _client.GetFromJsonAsync<List<WetherDto>>("WeatherForecast");
    }
}