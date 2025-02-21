using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySymptoms_Server.Presentation.Abstractions;
using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Application.Services.Product;
using TheGrace.Application.Services.ProductCategory;
using TheGrace.Contract.Utils;
using ContractProduct = TheGrace.Contract.Services.Product;
using System.Reflection;
using MediatR;

namespace TheGrace.API.Controllers.V1;

[ApiVersion(1)]
[AllowAnonymous]
public class ProductController : ApiController
{

    public ProductController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<ContractProduct.Response.ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts(
        string? searchTerm = null,
        int filterType = 0,
        bool filterIsInDelete = true,
        string? sortColumn = null,
        string? sortOrder = null,
        string? sortColumnAndOrder = null,
        int pageIndex = 1,
        int pageSize = 80)
    {
        var result = await Sender.Send(new ContractProduct.Query.GetProductsQuery(
            searchTerm,
            filterType,
            filterIsInDelete,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            SortOrderExtension.ConvertStringToSortOrderV2ToProduct(sortColumnAndOrder),
            pageIndex,
            pageSize
            ));

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(Result<ContractProduct.Response.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var result = await Sender.Send(new ContractProduct.Query.GetProductDetailQuery(productId));

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<ContractProduct.Response.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] ContractProduct.Command.CreateProductCommand request)
    {
        var result = await Sender.Send(request);

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(Result<ContractProduct.Response.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ContractProduct.Command.UpdateProductCommand request)
    {
        var requestUpdate = new ContractProduct.Command.UpdateProductCommand(productId, request.type, request.name, request.description, request.quantity, request.price);
        var result = await Sender.Send(requestUpdate);

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await Sender.Send(new ContractProduct.Command.DeleteProductCommand(productId));

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    //[HttpPost("multiple/type")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> DeleteProduct([FromBody] ContractProduct.Command.ChangeMultipleTypeOfProductCommand multipleTypeOfProduct)
    //{
    //    var result = await _productService.ChangeMultipleType(multipleTypeOfProduct);

    //    return result.IsFailure ? HandleFailure(result) : Ok(result);
    //}

    //[HttpPost("create/products")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateProducts()
    //{
    //    var result = await _productService.CreateProducts();

    //    return result.IsFailure ? HandleFailure(result) : Ok(result);
    //}
}
