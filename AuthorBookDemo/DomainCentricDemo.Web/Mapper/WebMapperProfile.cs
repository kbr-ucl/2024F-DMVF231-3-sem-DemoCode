using AutoMapper;
using DomainCentricDemo.Web.Infrastructure.Dto;
using DomainCentricDemo.Web.Pages.Book;

namespace DomainCentricDemo.Web.Mapper
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<BookViewModel, BookDto>(MemberList.Source);
            CreateMap<BookDto, BookViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Authors.Any() ? src.Authors.First().Id : string.Empty));
        }
    }
}
