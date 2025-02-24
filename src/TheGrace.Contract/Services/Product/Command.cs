using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Contract.Services.Product;
public class Command
{
    public record CreateProductCommand(int id, StatusEnum type, string name, string description, int quantity, decimal price, int productCategoryId) : ICommand<Response.ProductResponse>;

    public record UpdateProductCommand(int id, StatusEnum type, string name, string description, int quantity, decimal price) : ICommand<Response.ProductResponse>;

    public record ChangeMultipleTypeOfProductCommand(HashSet<int> ids, StatusEnum type) : ICommand;

    public record DeleteProductCommand(int id) : ICommand;
}
