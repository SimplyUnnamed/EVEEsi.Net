using EveEsi.Net.Enums.Client;

namespace EveEsi.Net.Config;

public class EsiEndpoint
{
	public EsiEndpoint(string endpointId, bool protectedEndpoint, HttpMethodType methodType, string routes,
		string? scope, ESI.Endpoints.RateLimitGroup? rateLimitGroup)
	{
		EndpointId = endpointId;
		ProtectedEndpoint = protectedEndpoint;
		MethodType = methodType;
		Route = routes;
		Scope = scope;
		RateLimitGroup = rateLimitGroup;
	}

	public string EndpointId { get; }
	public bool ProtectedEndpoint { get; }
	public HttpMethodType MethodType { get; }
	public string? Scope { get; }
	public string Route { get; }

	public ESI.Endpoints.RateLimitGroup? RateLimitGroup { get; }

	public TimeSpan? DefaultCacheExpiration { get; set; }
}
