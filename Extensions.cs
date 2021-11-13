using System.Collections.Generic;

namespace ExtraDevelopment.Utilities
{
    public static class Extensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default)
            => dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
    }
}