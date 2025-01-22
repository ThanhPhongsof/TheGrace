using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySymptoms_Server.Presentation.Abstractions;
using TheGrace.Application.Abstractions.Shared;
using TheGrace.Application.Services.Product;
using TheGrace.Application.Services.ProductCategory;
using TheGrace.Application.Utils;
using ContractProductCategories = TheGrace.Domain.Contract.ProductCategory;

namespace TheGrace.API.Controllers.V1;

[ApiVersion(1)]
[AllowAnonymous]
public class ProductCatgegoryController : ApiController
{
    private readonly IProductCategoryService _productCategoryService;

    public ProductCatgegoryController(
        IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpPost("create/productcategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductCategories()
    {
        var result = await _productCategoryService.CreateProductCategories();

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<ContractProductCategories.ProductCategoryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductCategories()
    {
        var result = await _productCategoryService.GetProductCategories();

        return Ok(result);
    }
}
