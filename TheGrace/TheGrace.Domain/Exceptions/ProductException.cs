using TheGrace.Domain.Exceptions.Commons;

namespace TheGrace.Domain.Exceptions;

public static class ProductException
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string ProductId)
            : base($"The product with productId [{ProductId}] was not found") { }
    }
}
