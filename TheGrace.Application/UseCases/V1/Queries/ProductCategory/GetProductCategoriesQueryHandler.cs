using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGrace.Application.Services.ProductCategory;
using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Contract.Services.ProductCategory;
using TheGrace.Domain.Abstractions.Repositories;
using Model = TheGrace.Domain.Entities;

namespace TheGrace.Application.UseCases.V1.Queries.ProductCategory;

internal sealed class GetProductCategoriesQueryHandler : IQueryHandler<Query.GetProductCategoriesQuery, IEnumerable<Response.ProductCategoryResponse>>
{
    private readonly IProductCategoryService _productCategoryService;

    public GetProductCategoriesQueryHandler(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    public async Task<Result<IEnumerable<Response.ProductCategoryResponse>>> Handle(Query.GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Result.Success(await _productCategoryService.GetProductCategories());
    }
}
