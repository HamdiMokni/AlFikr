using System.Text.Json;

namespace AlFikr.FrontendUI.Web.Extensions;

public static class SerializeHelperExtensions
{
    public static T ToEntity<T>(this string entity)
    {
        if(string.IsNullOrEmpty(entity))
            return default(T);

        return JsonSerializer.Deserialize<T>(entity);
    }

    public static string ToJson<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}
