using DomainCentricDemo.Web.Infrastructure.Dto;

namespace DomainCentricDemo.Web.Infrastructure
{
    public interface IBookApiProxy
    {
        Task CreateAsync(BookDto book);
        Task<BookDto?> GetAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(BookDto book);
        Task<IEnumerable<BookDto>?> GetAllAsync();
    }

    public interface IWetherApiProxy
    {
        Task<IEnumerable<WetherDto>?> GetAllAsync();
    }
}
