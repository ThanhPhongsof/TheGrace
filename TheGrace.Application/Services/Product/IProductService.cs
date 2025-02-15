using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Application.Abstractions.Shared;
using ContractProduct = TheGrace.Domain.Contract.Product;

namespace TheGrace.Application.Services.Product;

public interface IProductService
{
    Task<Result<PagedResult<ContractProduct.ProductResponse>>> GetProducts(ContractProduct.Query.GetProductsQuery request);

    Task<Result<ContractProduct.ProductResponse>> GetProduct(ContractProduct.Query.GetProductQuery request);

    Task<Result> DeleteProduct(ContractProduct.Command.DeleteProductCommand request);

    Task<Result> ChangeMultipleType(ContractProduct.Command.ChangeMultipleTypeOfProductCommand request);

    Task<Result<ContractProduct.ProductResponse>> CreateOrUpdateProduct(ContractProduct.Command.CreateOrUpdateProductCommand request);

    Task<Result> CreateProducts();
}
