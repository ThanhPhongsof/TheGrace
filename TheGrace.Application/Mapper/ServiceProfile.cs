using AutoMapper;
using TheGrace.Domain.Contract;
using TheGrace.Domain.Entities;

namespace TheGrace.Application.Mapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ProductCategory, ProductCategoryResponse>().ReverseMap();
    }
}
