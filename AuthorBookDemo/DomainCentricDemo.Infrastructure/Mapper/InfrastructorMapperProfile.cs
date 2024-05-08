using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Infrastructure.Mapper;

public class InfrastructorMapperProfile : Profile
{
    public InfrastructorMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, BookAuthorDto>(MemberList.Destination);
    }
}