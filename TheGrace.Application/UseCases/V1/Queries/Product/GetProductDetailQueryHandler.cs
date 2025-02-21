using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Contract.Services.Product;
using ContractCategory = TheGrace.Contract.Services.ProductCategory;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Exceptions.Commons;
using TheGrace.Persistence;
using Model = TheGrace.Domain.Entities;
using TheGrace.Application.Services.Product;

namespace TheGrace.Application.UseCases.V1.Queries.Product;
internal sealed class GetProductDetailQueryHandler : IQueryHandler<Query.GetProductDetailQuery, Response.ProductResponse>
{
    private readonly IProductService _productService;

    public GetProductDetailQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductById(request.id);

        return Result.Success(product);
    }
}
