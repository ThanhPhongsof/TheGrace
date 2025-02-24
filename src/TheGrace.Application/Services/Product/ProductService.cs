using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TheGrace.Application.Services.TimeZone;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Abstractions;
using TheGrace.Persistence;
using Model = TheGrace.Domain.Entities;
using TheGrace.Domain.Exceptions.Commons;
using ContractProduct = TheGrace.Contract.Services.Product;
using ContractCategoy = TheGrace.Contract.Services.ProductCategory;
using static TheGrace.Contract.Services.Product.Response;
using static TheGrace.Contract.Services.ProductCategory.Response;

namespace TheGrace.Application.Services.Product;
public class ProductService : IProductService
{
    private readonly IRepositoryBase<Model.Product, int> _productRepository;
    private readonly IRepositoryBase<Model.ProductCategory, int> _productCategoryRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IRepositoryBase<Model.Product, int> productRepository,
        IRepositoryBase<Model.ProductCategory, int> productCategoryRepository,
        IMapper mapper
        )
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _mapper = mapper;
    }

    public async Task<ProductResponse> GetProductById(int productId)
    {
        var product = await _productRepository.FindByIdAsync(productId);

        if (product is null)
        {
            throw new NotFoundException($"This product [{productId}] could not found!");
        }

        var productCategory = await _productCategoryRepository.FindByIdAsync(product.ProductCategoryId);
        var result = _mapper.Map<ProductResponse>(product);
        result.Category = _mapper.Map<ProductCategoryResponse>(productCategory);

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
