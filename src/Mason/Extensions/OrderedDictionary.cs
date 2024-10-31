namespace Mason.Extensions;

public static class OrderedDictionary
{
    public static bool TryGetValue<T>(this System.Collections.Specialized.OrderedDictionary dictionary, object key, out T? value)
    {
        value = (T?)dictionary[key];

        if (value is null)
        {
            return false;
        }

        return true;
    }
}
