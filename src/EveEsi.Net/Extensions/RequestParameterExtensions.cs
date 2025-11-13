using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Extensions;

public static class RequestParameterExtensions
{
	public static void ApplyPage(this RequestParameters requestParameters, int? page)
	{
		requestParameters.Query[ESI.Parameters.Query.Page] = page?.ToString() ?? "1";
	}
}
