using AutoMapper;
using Azure;
using TheGrace.Contract.Abstractions.Shared;
using ContractProduct = TheGrace.Contract.Services.Product;
using ContractProductCategory = TheGrace.Contract.Services.ProductCategory;
using TheGrace.Domain.Entities;

namespace TheGrace.Application.Mapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ProductCategory, ContractProductCategory.Response.ProductCategoryResponse>().ReverseMap();
        CreateMap<Product, ContractProduct.Response.ProductResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.Id))
            .ForMember(dest => dest.ProductCategoryId, opt => opt.MapFrom(p => p.ProductCategoryId))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(p => p.Image))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(p => p.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(p => p.Type))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(p => p.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(p => p.Price))
            .ForMember(dest => dest.IsInActive, opt => opt.MapFrom(p => p.IsInActive))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(p => p.CreatedAt))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(p => p.CreatedBy))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(p => p.UpdatedAt))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(p => p.UpdatedBy));
        CreateMap<PagedResult<Product>, PagedResult<ContractProduct.Response.ProductResponse>>().ReverseMap();
    }
}
