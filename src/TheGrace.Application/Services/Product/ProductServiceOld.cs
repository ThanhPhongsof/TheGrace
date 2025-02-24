using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Abstractions;
using Model = TheGrace.Domain.Entities;
using ContractProduct = TheGrace.Contract.Services.Product;
using ContractCategoy = TheGrace.Contract.Services.ProductCategory;
using TheGrace.Application.Services.TimeZone;
using TheGrace.Application.Services.ProductCategory;
using Microsoft.EntityFrameworkCore;
using TheGrace.Domain.Enumerations;
using System.Linq.Expressions;
using Azure.Core;
using System.Threading;
using TheGrace.Persistence;
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;
using System;
using TheGrace.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Eventing.Reader;
using TheGrace.Domain.Exceptions.Commons;
using Castle.Components.DictionaryAdapter.Xml;

namespace TheGrace.Application.Services.Product;

public class ProductServiceOld : IProductServiceOld
{
    private readonly IRepositoryBase<Model.Product, int> _productRepository;
    private readonly IRepositoryBase<Model.ProductCategory, int> _productCategoryRepository;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeZoneService _timeZoneService;

    public ProductServiceOld(
        IRepositoryBase<Model.Product, int> productRepository,
        IRepositoryBase<Model.ProductCategory, int> productCategoryRepository,
        ApplicationDbContext context,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ITimeZoneService timeZoneService
        )
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _unitOfWork = unitOfWork;
        _timeZoneService = timeZoneService;
        _context = context;
        _mapper = mapper;
    }

    //public async Task<Result> ChangeMultipleType(ContractProduct.Command.ChangeMultipleTypeOfProductCommand request)
    //{
    //    if (request.ids.Count == 0)
    //        throw new NotFoundException("Ids must be have least one value");

    //    var products = await _context.Products
    //                        .Where(p => request.ids.Contains(p.Id))
    //                        .ExecuteUpdateAsync(upt =>
    //                            upt.SetProperty(b => b.Type, request.type.Value)
    //                               .SetProperty(b => b.UpdatedAt, _timeZoneService.GetCurrentTime())
    //                               .SetProperty(b => b.UpdatedBy, "Sakura"));

    //    await _unitOfWork.CommitAsync();

    //    return Result.Success();
    //}

    public async Task<Result> CreateProducts()
    {
        var productCategories = await _productCategoryRepository.FindAll().ToListAsync();
        var productAny = await _productRepository.FindAll(x => x.Type == StatusEnum.Active).AnyAsync();

        if (productCategories.Count() == 0)
        {
            throw new NotImplementedException("Product categories don't have data");
        }

        if (productAny)
        {
            throw new NotImplementedException("Product already inserted");
        }

        var products = new List<Model.Product>();
        var quantityProducts = 1000;

        string createdBy = "Carol";
        string description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout";
        DateTimeOffset createdAt = _timeZoneService.GetCurrentTime();

        var random = new Random(); // Single Random instance

        var categorieImages = GetProductImages();

        foreach (var category in productCategories)
        {
            string image = GetProductImageSingle(categorieImages, category.Name);

            for (int i = 0; i < quantityProducts; i++)
            {
                var productCode = CreateProductCode(category.Name, i + 1);

                var itemProduct = new Model.Builder.ProductBuilderPattern.ProductBuilder()
                                     .SetType(StatusEnum.Active)
                                     .SetName($"{category.Name} {productCode}")
                                     .SetCode(productCode)
                                     .SetQuantity(random.Next(30, 1000))
                                     .SetPrice(random.Next(150000, 1000000))
                                     .SetSoftDelete(true)
                                     .SetDescription(description)
                                     .SetImage(image)
                                     .SetCreatedBy(createdBy, createdAt)
                                     .SetUpdatedBy(createdBy, createdAt)
                                     .SetProductCategory(category)
                                     .Build();

                products.Add(itemProduct);
            }
        }

        _productRepository.AddMultiple(products);

        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

    public async Task<ContractProduct.Response.ProductResponse> GetProductById(int productId)
    {
        var product = await _productRepository.FindByIdAsync(productId);

        if (product is null)
        {
            throw new NotFoundException($"This product [{productId}] could not found!");
        }

        var productCategory = await _productCategoryRepository.FindByIdAsync(product.ProductCategoryId);
        var result = _mapper.Map<ContractProduct.Response.ProductResponse>(product);
        result.Category = _mapper.Map<ContractCategoy.Response.ProductCategoryResponse>(productCategory);

        return result;
    }

    public string CreateProductCode(string productCategoryName, int position)
    {
        // Calculate the unique timestamp-based component
        string timestamp = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 1603791323).ToString();
        var Guid = SequentialGuid.NewGuid();

        // Return the timestamp if the product category name is null or empty
        if (string.IsNullOrWhiteSpace(productCategoryName))
        {
            return timestamp;
        }

        // Extract the first word or segment before any comma in the category name
        string prefix = productCategoryName.Trim()[0].ToString();

        return $"{prefix}{timestamp}{Guid.ToString().Split('-')[1]}{position}";
    }

    public Dictionary<string, string> GetProductImages()
    {
        var categoryImages = new Dictionary<string, string>
        {
            { "Phone", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/12/02/iphone-16-pro-max-sa-mac-1.png" },
            { "Laptop", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/11/05/macbook-pro-16-inch-m4-pro-den-1.png" },
            { "SmartWatch", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/12/05/watch-se-2024-lte-44mm-anh-sao-1.png" },
            { "Tablet", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/11/04/ipad-mini-7-xam-wifi-1.png" }
        };

        return categoryImages;
    }

    public string GetProductImageSingle(Dictionary<string, string> categoryImages, string name)
    {
        string image = categoryImages.ContainsKey(name) ? categoryImages[name] : "";

        return image;
    }
}
