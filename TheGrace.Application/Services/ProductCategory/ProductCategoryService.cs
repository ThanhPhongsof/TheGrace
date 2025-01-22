using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using TheGrace.Application.Abstractions.Shared;
using TheGrace.Application.Services.TimeZone;
using TheGrace.Domain.Abstractions;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Contract.ProductCategory;
using TheGrace.Domain.Entities.Builder.ProductBuilderPattern;
using TheGrace.Domain.Entities.Builder.ProductCategoryBuilderPattern;
using TheGrace.Domain.Enumerations;
using TheGrace.Persistence;
using Model = TheGrace.Domain.Entities;

namespace TheGrace.Application.Services.ProductCategory;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IRepositoryBase<Model.ProductCategory, int> _productCategoryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeZoneService _timeZoneService;

    public ProductCategoryService(
        IRepositoryBase<Model.ProductCategory, int> productCategoryRepository,
        ITimeZoneService timeZoneService,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _productCategoryRepository = productCategoryRepository;
        _timeZoneService = timeZoneService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> CreateProductCategories()
    {
        var checkAll = await _productCategoryRepository.FindAll(x => x.Name == "Phone").AnyAsync();

        if (checkAll) throw new Exception("Data already inserted");

        string createdBy = "Carol";
        string description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout";
        DateTimeOffset createdAt = _timeZoneService.GetCurrentTime();

        var productCategories = new List<Model.ProductCategory>();

        var categoryPhone = new ProductCategoryBuilder()
                        .SetName("Phone")
                        .SetDescription(description)
                        .SetType(StatusEnum.Active)
                        .SetSoftDelete(true)
                        .SetCreatedBy(createdBy, createdAt)
                        .SetUpdatedBy(createdBy, createdAt)
                        .Build();

        var categoryLaptop = new ProductCategoryBuilder()
                        .SetName("Laptop")
                        .SetDescription(description)
                        .SetType(StatusEnum.Active)
                        .SetSoftDelete(true)
                        .SetCreatedBy(createdBy, createdAt)
                        .SetUpdatedBy(createdBy, createdAt)
                        .Build();

        var categorySmartWatch = new ProductCategoryBuilder()
                        .SetName("SmartWatch")
                        .SetDescription(description)
                        .SetType(StatusEnum.Active)
                        .SetSoftDelete(true)
                        .SetCreatedBy(createdBy, createdAt)
                        .SetUpdatedBy(createdBy, createdAt)
                        .Build();

        var categoryTablet = new ProductCategoryBuilder()
                        .SetName("Tablet")
                        .SetDescription(description)
                        .SetType(StatusEnum.Active)
                        .SetSoftDelete(true)
                        .SetCreatedBy(createdBy, createdAt)
                        .SetUpdatedBy(createdBy, createdAt)
                        .Build();

        productCategories.Add(categoryPhone);
        productCategories.Add(categoryLaptop);
        productCategories.Add(categoryTablet);
        productCategories.Add(categorySmartWatch);

        _productCategoryRepository.AddMultiple(productCategories);

        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ProductCategoryResponse>>> GetProductCategories()
    {
        var productCategories = _productCategoryRepository.FindAll().AsAsyncEnumerable();
        var result = _mapper.Map<IEnumerable<ProductCategoryResponse>>(productCategories);

        return Result.Success(result);
    }
}
