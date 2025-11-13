using System.Collections.Specialized;
using System.Text;
using EveEsi.Net.Extensions;

namespace EveEsi.Net.Utilities;

internal static class EndpointKeyGenerator
{
	public static string GetKey(string endpointId)
	{
		return GetKey(endpointId, new NameValueCollection());
	}

	public static string GetKey(string endpointId, NameValueCollection routeValues)
	{
		return GetKey(endpointId, routeValues, new NameValueCollection());
	}

	public static string GetKey(string endpointId, NameValueCollection routeValues, NameValueCollection queryValues,
		IEnumerable<string>? ignoreKeys = null)
	{
		return GenerateRoute(endpointId, routeValues, queryValues, ignoreKeys).ToMd5Hash();
	}


	public static string GenerateRoute(string endpointId, NameValueCollection routeValues,
		NameValueCollection queryValues, IEnumerable<string>? ignoreKeys = null)
	{
		ArgumentNullException.ThrowIfNull(routeValues);
		ArgumentNullException.ThrowIfNull(queryValues);
		ArgumentNullException.ThrowIfNull(endpointId);

		StringBuilder sb = new();

		sb.Append(endpointId);
		sb.Append("::");

		if (ignoreKeys == null)
		{
			ignoreKeys = [];
		}

		foreach (string? key in routeValues.AllKeys.Where(x => !ignoreKeys.Contains(x)))
		{
			sb.Append($"{key}={routeValues[key]}");
		}

		foreach (string? key in queryValues.AllKeys.Where(x => !ignoreKeys.Contains(x)))
		{
			sb.Append($"{key}={queryValues[key]}");
		}

		return sb.ToString();
	}
}
