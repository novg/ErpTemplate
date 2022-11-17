using Application.Models;
using AutoMapper;
using Domain.Models;

namespace Application;

public class MappingModelsProfile : Profile
{
    protected MappingModelsProfile()
    {
        CreateMap<Book, BookDto>()
            .ReverseMap();
        CreateMap<Order, OrderDto>()
            .ReverseMap();
    }
}