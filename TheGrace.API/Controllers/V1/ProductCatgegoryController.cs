using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySymptoms_Server.Presentation.Abstractions;
using TheGrace.Application.Services.Product;
using TheGrace.Application.Services.ProductCategory;
using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Contract.Services.ProductCategory;

namespace TheGrace.API.Controllers.V1;

[ApiVersion(1)]
[AllowAnonymous]
public class ProductCatgegoryController : ApiController
{
    public ProductCatgegoryController(ISender sender) :base(sender)
    {
    }

    //[HttpPost("create/productcategories")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateProductCategories()
    //{
    //    var result = await _productCategoryService.CreateProductCategories();

    //    return result.IsFailure ? HandleFailure(result) : Ok(result);
    //}

    [HttpGet]
    //[ProducesResponseType(typeof(Result<IEnumerable<Response.ProductCategoryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductCategories()
    {
        var result = await Sender.Send(new Query.GetProductCategoriesQuery());

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }
}
