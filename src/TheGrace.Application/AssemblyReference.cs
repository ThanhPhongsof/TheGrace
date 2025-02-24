using System.Reflection;
using TheGrace.Application.UseCases.V1.Queries.ProductCategory;

namespace TheGrace.Application;

//public class AssemblyReference
//{
//    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
//}
public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(GetProductCategoriesQueryHandler).Assembly;
}

