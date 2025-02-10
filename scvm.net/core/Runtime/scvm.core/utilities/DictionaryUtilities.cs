using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace scvm.core.utilities
{
	public static class DictionaryUtilities
	{
		public static bool TryReverseQuery<T, V>(this Dictionary<T, V> dict, V value, [MaybeNullWhen(false)] out T key)
		{
			foreach (var item in dict.Keys)
			{
				if (dict[item]?.Equals(value) ?? false)
				{
					key = item;
					return true;
				}
			}

			key = default;
			return false;
		}
		public static T ReverseQuery<T, V>(this Dictionary<T, V> dict, V value)
		{
			foreach (var item in dict.Keys)
			{
				if (dict[item]?.Equals(value) ?? false)
				{
					return item;
				}
			}
#pragma warning disable CS8603 // Possible null reference return.
			return default;
#pragma warning restore CS8603 // Possible null reference return.
		}
	}
}
