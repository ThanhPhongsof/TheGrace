using System.Reflection;

namespace TheGrace.API;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
