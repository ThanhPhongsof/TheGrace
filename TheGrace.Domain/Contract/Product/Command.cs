using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Contract.Product;
public class Command
{
    public record CreateOrUpdateProductCommand(int id, StatusEnum type, string name, string description, int quantity, decimal price, int productCategoryId);

    public record UpdateProductCommand(StatusEnum type, string name, string description, int quantity, decimal price);

    public record ChangeMultipleTypeOfProductCommand(HashSet<int> ids, StatusEnum type);

    public record DeleteProductCommand(int id);
}
