using System.Reflection;

namespace TheGrace.Application;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
