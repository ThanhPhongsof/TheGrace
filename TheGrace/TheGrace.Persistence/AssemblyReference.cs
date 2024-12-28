using System.Reflection;

namespace TheGrace.Persistence;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
