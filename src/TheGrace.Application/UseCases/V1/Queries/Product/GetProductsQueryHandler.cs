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
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheGrace.Contract.Abstractions.Shared;
using Model = TheGrace.Domain.Entities;
using Contracts = TheGrace.Contract.Services;
using TheGrace.Application.Services.ProductCategory;

namespace TheGrace.Application.UseCases.V1.Queries.Product;

internal sealed class GetProductsQueryHandler : IQueryHandler<Contracts.Product.Query.GetProductsQuery, PagedResult<Contracts.Product.Response.ProductResponse>>
{
    private readonly IRepositoryBase<Model.Product, int> _productRepository;
    private readonly IProductCategoryService _productCategoryService;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(
        IRepositoryBase<Model.Product, int> productRepository,
        IProductCategoryService productCategoryService,
        ApplicationDbContext context,
        IMapper mapper
        )
    {
        _productRepository = productRepository;
        _productCategoryService = productCategoryService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Contracts.Product.Response.ProductResponse>>> Handle(Contracts.Product.Query.GetProductsQuery request, CancellationToken cancellationToken)
    {
        var categories = await _productCategoryService.GetProductCategories();

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
                    productsQuery += item.Value == Contracts.SortOrder.Descending
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

            var result = _mapper.Map<PagedResult<Contracts.Product.Response.ProductResponse>>(productPagedResult);
            if (result.TotalCount > 0)
            {
                result.Items.ForEach(x =>
                {
                    x.Category = categories.FirstOrDefault(p => p.Id == x.ProductCategoryId);
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

            productsQuery = request.SortOrder == Contracts.SortOrder.Descending
            ? productsQuery.OrderByDescending(GetSortProperty(request.SortColumn))
            : productsQuery.OrderBy(GetSortProperty(request.SortColumn));

            var products = await PagedResult<Model.Product>.CreateAsync(productsQuery,
                request.PageIndex,
                request.PageSize);

            var result = _mapper.Map<PagedResult<Contracts.Product.Response.ProductResponse>>(products);
            if (result.TotalCount > 0)
            {
                result.Items.ForEach(x =>
                {
                    x.Category = categories.FirstOrDefault(p => p.Id == x.ProductCategoryId);
                });
            }

            return Result.Success(result);
        }
    }

    private static Expression<Func<Model.Product, object>> GetSortProperty(string sortColumn)
         => sortColumn?.ToLower() switch
         {
             "name" => product => product.Name,
             "price" => product => product.Price,
             "description" => product => product.Description,
             _ => product => product.Id
         };
}
