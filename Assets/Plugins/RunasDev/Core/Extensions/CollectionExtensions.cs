using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunasDev.Core.Extensions
{
    public static class CollectionTools
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }
        
        public static bool IsNullOrEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }
        
        public static TData GetValueOrDefault<TKey, TData>(this IDictionary<TKey, TData> dictionary, TKey key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return default;
            }
        }
        
        public static TData GetValueOrDefault<TKey, TData>(this IDictionary<TKey, TData> dictionary, TKey key, TData defaultValue)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return defaultValue;
            }
        }

        public static bool InRange<T>(this T[,] array, Vector2Int pos)
        {
            return array.InRange(pos.x, pos.y);
        }
        
        public static bool InRange<T>(this T[,] array, int x, int y)
        {
            return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
        }
    }
}