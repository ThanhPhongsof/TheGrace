using TheGrace.Domain.Exceptions.Commons;

namespace TheGrace.Domain.Exceptions;

public static class ProductCategoryException
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(string ProductCategoryId)
            : base($"The product category with productCategoryId [{ProductCategoryId}] was not found") { }
    }
}
