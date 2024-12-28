using System.Collections;

namespace TheGrace.Application.DependencyInjection.Extensions;

public static class ObjectExtensions
{
    public static Dictionary<string, string> MapToDictionary(this object source, string name)
    {
        var dictionary = new Dictionary<string, string>();
        MapToDictionaryInternal(dictionary, source, name);
        return dictionary;
    }

    private static void MapToDictionaryInternal(
        Dictionary<string, string> dictionary, object source, string name)
    {
        var properties = source.GetType().GetProperties();
        foreach (var p in properties)
        {
            var key = !string.IsNullOrEmpty(name) ? name + "." + p.Name : p.Name;
            object value = p.GetValue(source, null);
            if (value == null)
                continue;

            Type valueType = value.GetType();

            if (valueType.IsPrimitive || valueType == typeof(string))
            {
                dictionary[key] = value.ToString();
            }
            else if (value is IEnumerable enumerable)
            {
                var i = 0;
                foreach (object o in enumerable)
                {
                    MapToDictionaryInternal(dictionary, o, key + "[" + i + "]");
                    i++;
                }
            }
            else
            {
                MapToDictionaryInternal(dictionary, value, key);
            }
        }
    }
}