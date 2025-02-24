using System.Reflection;

namespace TheGrace.Domain;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
