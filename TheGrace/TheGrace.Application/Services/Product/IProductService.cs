using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Application.Abstractions.Shared;

namespace TheGrace.Application.Services.Product;

public interface IProductService
{
    Task<Result> CreateProducts();
}
