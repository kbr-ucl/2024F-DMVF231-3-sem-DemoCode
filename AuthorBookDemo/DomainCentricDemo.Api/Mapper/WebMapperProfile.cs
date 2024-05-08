using AutoMapper;
using DomainCentricDemo.Application.Dto;

namespace DomainCentricDemo.Api.Mapper;

public class WebMapperProfile : Profile
{
    public WebMapperProfile()
    {
        CreateMap<BookDto, BookCreateCommandRequestDto>();
        CreateMap<BookDto, BookUpdateRequestDto>();
    }
}