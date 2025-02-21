using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Enumerations;
using static TheGrace.Contract.Services.ProductCategory.Response;

namespace TheGrace.Contract.Services.Product;

public class Response
{
    public class ProductResponse : ResponseCommon
    {
        public int Id { get; set; }

        public int ProductCategoryId { get; set; }

        public StatusEnum Type { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ProductCategoryResponse? Category { get; set; }
    }
}
