using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySymptoms_Server.Presentation.Abstractions;
using TheGrace.Application.Services.Product;
using TheGrace.Application.Services.ProductCategory;

namespace TheGrace.API.Controllers.V1;

[ApiVersion(1)]
[AllowAnonymous]
public class ProductController : ApiController
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly IProductService _productService;

    public ProductController(
        IProductCategoryService productCategoryService,
        IProductService productService)
    {
        _productCategoryService = productCategoryService;
        _productService = productService;
    }

    [HttpPost("create/productcategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductCategories()
    {
        var result = await _productCategoryService.CreateProductCategories();

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpPost("create/products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProducts()
    {
        var result = await _productService.CreateProducts();

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }
}
