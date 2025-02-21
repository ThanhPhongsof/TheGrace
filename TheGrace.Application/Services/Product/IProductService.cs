using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheGrace.Contract.Services.Product.Response;

namespace TheGrace.Application.Services.Product;
public interface IProductService
{
    Task<ProductResponse> GetProductById(int productId);

    string GetProductImageSingle(Dictionary<string, string> categoryImages, string name);

    string CreateProductCode(string productCategoryName, int position);

    Dictionary<string, string> GetProductImages();
}
