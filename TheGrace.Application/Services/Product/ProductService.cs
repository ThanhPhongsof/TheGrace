using TheGrace.Application.Abstractions.Shared;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Abstractions;
using Model = TheGrace.Domain.Entities;
using TheGrace.Application.Services.TimeZone;
using TheGrace.Application.Services.ProductCategory;
using TheGrace.Domain.Entities.Builder.ProductBuilderPattern;
using Microsoft.EntityFrameworkCore;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Application.Services.Product;

public class ProductService : IProductService
{
    private readonly IRepositoryBase<Model.Product, int> _productRepository;
    private readonly IRepositoryBase<Model.ProductCategory, int> _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeZoneService _timeZoneService;

    public ProductService(
        IRepositoryBase<Model.Product, int> productRepository,
        IRepositoryBase<Model.ProductCategory, int> productCategoryRepository,
        IUnitOfWork unitOfWork,
        ITimeZoneService timeZoneService
        )
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _unitOfWork = unitOfWork;
        _timeZoneService = timeZoneService;
    }

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
                var Guid = SequentialGuid.NewGuid();

                var productCode = CreateProductCode(category.Name, i + 1);

                var itemProduct = new ProductBuilder()
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

    private string CreateProductCode(string productCategoryName, int position)
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

    private Dictionary<string, string> GetProductImages()
    {
        var categoryImages = new Dictionary<string, string>
        {
            { "Phone", "https://drive.google.com/drive/folders/1Piva1ISzZAEeD_4przdwjBESNv3JxpD4" },
            { "Laptop", "https://drive.google.com/drive/folders/1QZq0HBpWfgNkh3WlgdXKK-xb6dtoywyc" },
            { "SmartWatch", "https://drive.google.com/drive/folders/1I82E5TurjOPNS0IkeCZz1CxCV6ye9AlV" },
            { "Tablet", "https://drive.google.com/drive/folders/19ucfJKPxgMKJiCZkPkcqrQodN8UY0RUi" }
        };

        return categoryImages;
    }

    private string GetProductImageSingle(Dictionary<string, string> categoryImages, string name)
    {
        string image = categoryImages.ContainsKey(name) ? categoryImages[name] : "";

        return image;
    }
}
