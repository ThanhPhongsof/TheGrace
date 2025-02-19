using TheGrace.Application.Abstractions.Shared;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Abstractions;
using Model = TheGrace.Domain.Entities;
using ContractProduct = TheGrace.Domain.Contract.Product;
using ContractCategoy = TheGrace.Domain.Contract.ProductCategory;
using TheGrace.Application.Services.TimeZone;
using TheGrace.Application.Services.ProductCategory;
using Microsoft.EntityFrameworkCore;
using TheGrace.Domain.Enumerations;
using System.Linq.Expressions;
using Azure.Core;
using System.Threading;
using TheGrace.Domain.Contract;
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

public class ProductService : IProductService
{
    private readonly IRepositoryBase<Model.Product, int> _productRepository;
    private readonly IRepositoryBase<Model.ProductCategory, int> _productCategoryRepository;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeZoneService _timeZoneService;

    public ProductService(
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

    public async Task<Result<PagedResult<ContractProduct.ProductResponse>>> GetProducts(ContractProduct.Query.GetProductsQuery request)
    {
        var categoy = await _productCategoryRepository.FindAll().ToListAsync();
        var categoyMapper = _mapper.Map<List<ContractCategoy.ProductCategoryResponse>>(categoy);

        if (request.SortColumnAndOrder.Any()) // =>>  Raw Query when order by multi column
        {
            var PageIndex = request.PageIndex <= 0 ? PagedResult<Model.Product>.DefaultPageIndex : request.PageIndex;
            var PageSize = request.PageSize <= 0
                ? PagedResult<Model.Product>.DefaultPageSize
                : request.PageSize > PagedResult<Model.Product>.UpperPageSize
                ? PagedResult<Model.Product>.UpperPageSize : request.PageSize;

            // ============================================
            string where = "WHERE";
            where += request.FilterIsInDelete ? $" IsInActive = 1" : $" IsInActive = 0";
            where += request.FilterType > 0 ? $" AND Type = {request.FilterType}" : "";
            where += string.IsNullOrWhiteSpace(request.SearchTerm)
                    ? ""
                    : $@" AND ({nameof(Model.Product.Name)} LIKE '%{request.SearchTerm}%' OR {nameof(Model.Product.Description)} LIKE '%{request.SearchTerm}%') ";

            string productsQuery = $@"SELECT * FROM Products {where} ORDER BY";

            if (request.SortColumnAndOrder.Count == 0)
            {
                productsQuery += " Id DESC ";
            }
            else
            {
                foreach (var item in request.SortColumnAndOrder)
                    productsQuery += item.Value == SortOrder.Descending
                        ? $"{item.Key} DESC, "
                        : $"{item.Key} ASC, ";
            }

            productsQuery += $" OFFSET {(PageIndex - 1) * PageSize} ROWS FETCH NEXT {PageSize} ROWS ONLY";

            var products = await _context.Products.FromSqlRaw(productsQuery).ToListAsync(cancellationToken: default);

            var totalCount = await _context.Products.CountAsync(cancellationToken: default);

            var productPagedResult = PagedResult<Model.Product>.Create(products,
                PageIndex,
                PageSize,
                totalCount);

            var result = _mapper.Map<PagedResult<ContractProduct.ProductResponse>>(productPagedResult);
            if (result.TotalCount > 0)
            {
                result.Items.ForEach(x =>
                {
                    x.Category = categoyMapper.FirstOrDefault(p => p.Id == x.ProductCategoryId);
                });
            }

            return Result.Success(result);
        }
        else // =>> Entity Framework Core
        {
            var productsQuery = _productRepository.FindAll();

            if (string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                productsQuery = productsQuery.Where(x =>
                    EF.Functions.Like(x.Name, $"%{request.SearchTerm}%") ||
                    EF.Functions.Like(x.Description, $"%{request.SearchTerm}%"));
            }

            productsQuery = productsQuery.Where(x =>
                (request.FilterType == 0 || x.Type == request.FilterType)
                && x.IsInActive == request.FilterIsInDelete);

            productsQuery = request.SortOrder == SortOrder.Descending
            ? productsQuery.OrderByDescending(GetSortProperty(request))
            : productsQuery.OrderBy(GetSortProperty(request));

            var products = await PagedResult<Model.Product>.CreateAsync(productsQuery,
                request.PageIndex,
                request.PageSize);

            var result = _mapper.Map<PagedResult<ContractProduct.ProductResponse>>(products);
            if (result.TotalCount > 0)
            {
                result.Items.ForEach(x =>
                {
                    x.Category = categoyMapper.FirstOrDefault(p => p.Id == x.ProductCategoryId);
                });
            }

            return Result.Success(result);
        }
    }

    public async Task<Result<ContractProduct.ProductResponse>> GetProduct(ContractProduct.Query.GetProductQuery request)
    {
        var product = await _productRepository.FindByIdAsync(request.id);

        if (product is null)
        {
            throw new NotFoundException($"This product [{request.id}] could not found!");
        }

        var productCategory = await _productCategoryRepository.FindByIdAsync(product.ProductCategoryId);
        var result = _mapper.Map<ContractProduct.ProductResponse>(product);
        result.Category = _mapper.Map<ContractCategoy.ProductCategoryResponse>(productCategory);

        return Result.Success(result);
    }

    public async Task<Result<ContractProduct.ProductResponse>> CreateOrUpdateProduct(ContractProduct.Command.CreateOrUpdateProductCommand request)
    {
        var product = await _productRepository.FindByIdAsync(request.id);
        var productId = 0;

        DateTimeOffset createdAt = _timeZoneService.GetCurrentTime();

        if (product is null)
        {
            var category = await _productCategoryRepository.FindByIdAsync(request.productCategoryId);
            if (category is null)
                throw new InvalidDataException("ProductCategory could not found!");

            var random = new Random(); // Single Random instance

            var productCode = CreateProductCode(category.Name, random.Next(1, 99));
            var categorieImages = GetProductImages();
            string image = GetProductImageSingle(categorieImages, category.Name);
            string createdBy = "Candy";

            var itemProduct = new Model.Builder.ProductBuilderPattern.ProductBuilder()
                                 .SetType(StatusEnum.Active)
                                 .SetName(request.name)
                                 .SetCode(productCode)
                                 .SetQuantity(Math.Max(request.quantity, 1))
                                 .SetPrice(Math.Max(request.price, 100000))
                                 .SetSoftDelete(true)
                                 .SetDescription(request.description ?? "...")
                                 .SetImage(image)
                                 .SetCreatedBy(createdBy, createdAt)
                                 .SetUpdatedBy(createdBy, createdAt)
                                 .SetProductCategory(category)
                                 .Build();

            _productRepository.Add(itemProduct);

            await _unitOfWork.CommitAsync();
            productId = itemProduct.Id;
        }
        else
        {
            string updatedBy = "Arvix";
            string empty = "...";

            product.Update(
                request.type,
                request.name ?? empty,
                request.description ?? empty,
                Math.Max(request.price, 100000),
                Math.Max(request.quantity, 1),
                updatedBy,
                createdAt);
            _productRepository.Update(product);

            await _unitOfWork.CommitAsync();
            productId = product.Id;
        }

        return await GetProduct(new ContractProduct.Query.GetProductQuery(productId));
    }

    public async Task<Result> DeleteProduct(ContractProduct.Command.DeleteProductCommand request)
    {
        var products = await _context.Products
                            .Where(p => p.Id == request.id)
                            .ExecuteUpdateAsync(upt =>
                                upt.SetProperty(b => b.IsInActive, false)
                                   .SetProperty(b => b.UpdatedAt, _timeZoneService.GetCurrentTime())
                                   .SetProperty(b => b.UpdatedBy, "Kang"));

        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> ChangeMultipleType(ContractProduct.Command.ChangeMultipleTypeOfProductCommand request)
    {
        if (request.ids.Count == 0)
            throw new NotFoundException("Ids must be have least one value");

        var products = await _context.Products
                            .Where(p => request.ids.Contains(p.Id))
                            .ExecuteUpdateAsync(upt =>
                                upt.SetProperty(b => b.Type, request.type.Value)
                                   .SetProperty(b => b.UpdatedAt, _timeZoneService.GetCurrentTime())
                                   .SetProperty(b => b.UpdatedBy, "Sakura"));

        await _unitOfWork.CommitAsync();

        return Result.Success();
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
            { "Phone", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/12/02/iphone-16-pro-max-sa-mac-1.png" },
            { "Laptop", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/11/05/macbook-pro-16-inch-m4-pro-den-1.png" },
            { "SmartWatch", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/12/05/watch-se-2024-lte-44mm-anh-sao-1.png" },
            { "Tablet", "https://cdn.hoanghamobile.com/i/previewV2/Uploads/2024/11/04/ipad-mini-7-xam-wifi-1.png" }
        };

        return categoryImages;
    }

    private string GetProductImageSingle(Dictionary<string, string> categoryImages, string name)
    {
        string image = categoryImages.ContainsKey(name) ? categoryImages[name] : "";

        return image;
    }

    private static Expression<Func<Model.Product, object>> GetSortProperty(ContractProduct.Query.GetProductsQuery request)
         => request.SortColumn?.ToLower() switch
         {
             "name" => product => product.Name,
             "price" => product => product.Price,
             "description" => product => product.Description,
             _ => product => product.Id
             //_ => product => product.CreatedDate // Default Sort Descending on CreatedDate column
         };
}
