using EveEsi.Net.Enums.Client;

namespace EveEsi.Net.Config;

public class EsiEndpointBuilder
{
	public static string[] _validMethodTypes =
	{
		HttpMethod.Put.Method, HttpMethod.Post.Method, HttpMethod.Get.Method, HttpMethod.Delete.Method
	};

	public EsiEndpointBuilder(string endpointId)
	{
		EndpointId = endpointId;
	}


	public string EndpointId { get; set; }
	public HttpMethodType HttpMethodType { get; set; }
	public bool AuthenticatedEndpoint { get; set; }
	public string? Scope { get; set; }
	public string Route { get; set; } = null!;

	public TimeSpan? TimedCacheExpiry { get; set; }
	public TimeOnly? DailyCacheExpiry { get; set; }

	public ESI.Endpoints.RateLimitGroup? RateLimitGroup { get; set; }

	public EsiEndpoint Build()
	{
		Validate();

		return new EsiEndpoint(EndpointId, AuthenticatedEndpoint, HttpMethodType, Route, Scope, RateLimitGroup);
	}

	private void Validate()
	{
		if (AuthenticatedEndpoint && (string.IsNullOrEmpty(Scope) || !ESI.EsiScopes.Contains(Scope)))
		{
			throw new InvalidOperationException("An authenticated endpoint cannot be created without a valid scope");
		}

		if (Route is null)
		{
			throw new InvalidOperationException("At least one route must be configured for the esi endpoint.");
		}

		if (!_validMethodTypes.Contains(HttpMethodType.ToString(), StringComparer.OrdinalIgnoreCase))
		{
			throw new InvalidOperationException("Not supported HTTP method type: " + HttpMethodType);
		}
	}
}
