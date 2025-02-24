using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrace.Application.Services.Product;

public interface IProductServiceOld
{
    //Task<Result<PagedResult<ContractProduct.ProductResponse>>> GetProducts(ContractProduct.Query.GetProductsQuery request);

    //Task<Result<ContractProduct.ProductResponse>> GetProduct(ContractProduct.Query.GetProductQuery request);

    //Task<Result> DeleteProduct(ContractProduct.Command.DeleteProductCommand request);

    //Task<Result> ChangeMultipleType(ContractProduct.Command.ChangeMultipleTypeOfProductCommand request);

    //Task<Result<ContractProduct.ProductResponse>> CreateOrUpdateProduct(ContractProduct.Command.CreateOrUpdateProductCommand request);

    //Task<Result> CreateProducts();

    string GetProductImageSingle(Dictionary<string, string> categoryImages, string name);

    string CreateProductCode(string productCategoryName, int position);

    Dictionary<string, string> GetProductImages();
}
