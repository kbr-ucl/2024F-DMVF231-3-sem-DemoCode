using AutoMapper;
using DomainCentricDemo.Application.Dto;
using DomainCentricDemo.Domain;

namespace DomainCentricDemo.Application.Mapper;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<BookCreateCommandRequestDto, Book>();
    }
}