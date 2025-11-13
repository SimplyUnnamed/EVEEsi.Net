using System.Collections.Specialized;

namespace EveEsi.Net.Extensions;

public static class EnumerableExtensions
{
	public static NameValueCollection ToNameValueCollection<TKey, TValue>(
		this IEnumerable<KeyValuePair<TKey, TValue>> values)
	{
		NameValueCollection nv = new();
		foreach (KeyValuePair<TKey, TValue> pair in values)
		{
			nv.Add(pair.Key?.ToString(), pair.Value?.ToString());
		}

		return nv;
	}
}
