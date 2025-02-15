using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySymptoms_Server.Presentation.Abstractions;
using TheGrace.Application.Abstractions.Shared;
using TheGrace.Application.Services.Product;
using TheGrace.Application.Services.ProductCategory;
using TheGrace.Application.Utils;
using ContractProduct = TheGrace.Domain.Contract.Product;

namespace TheGrace.API.Controllers.V1;

[ApiVersion(1)]
[AllowAnonymous]
public class ProductController : ApiController
{
    private readonly IProductService _productService;

    public ProductController(
        IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("create/products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProducts()
    {
        var result = await _productService.CreateProducts();

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<ContractProduct.ProductResponse>>), StatusCodes.Status200OK)]
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
        var request = new ContractProduct.Query.GetProductsQuery(
            searchTerm,
            filterType,
            filterIsInDelete,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            SortOrderExtension.ConvertStringToSortOrderV2ToProduct(sortColumnAndOrder),
            pageIndex,
            pageSize);
        var result = await _productService.GetProducts(request);

        return Ok(result);
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(Result<ContractProduct.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var result = await _productService.GetProduct(new ContractProduct.Query.GetProductQuery(productId));

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<ContractProduct.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] ContractProduct.Command.CreateOrUpdateProductCommand request)
    {
        var result = await _productService.CreateOrUpdateProduct(request);

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(Result<ContractProduct.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ContractProduct.Command.UpdateProductCommand request)
    {
        var requestUpdate = new ContractProduct.Command.CreateOrUpdateProductCommand(productId, request.type, request.name, request.description, request.quantity, request.price, 0);
        var result = await _productService.CreateOrUpdateProduct(requestUpdate);

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await _productService.DeleteProduct(new ContractProduct.Command.DeleteProductCommand(productId));

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }

    [HttpPost("multiple/type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProduct([FromBody] ContractProduct.Command.ChangeMultipleTypeOfProductCommand multipleTypeOfProduct)
    {
        var result = await _productService.ChangeMultipleType(multipleTypeOfProduct);

        return result.IsFailure ? HandleFailure(result) : Ok(result);
    }
}
