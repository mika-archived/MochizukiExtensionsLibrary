/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace Mochizuki.ExtensionsLibrary.Runtime.Extensions
{
    internal static class DictionaryExtensions
    {
        public static TValue GetValueSafe<TKey, TValue>(this Dictionary<TKey, TValue> obj, TKey key)
        {
            return obj.GetValueSafeWithDefault(key, default);
        }

        public static TValue GetValueSafeWithDefault<TKey, TValue>(this Dictionary<TKey, TValue> obj, TKey key, TValue @default)
        {
            if (obj.ContainsKey(key))
                return obj[key];
            return @default;
        }
    }
}