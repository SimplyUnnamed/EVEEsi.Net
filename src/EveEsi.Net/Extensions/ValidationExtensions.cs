namespace EveEsi.Net.Extensions;

internal static class ValidationExtensions
{
	public static bool ListWithinSize<T>(this IEnumerable<T>? list, int max, int? min = 1)
	{
		if (list == null)
		{
			return true;
		}

		int listLength = list.Count();
		return listLength >= min && listLength <= max;
	}
}
