using System.Diagnostics.CodeAnalysis;
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
	public AuthenticatedEndpoint? AuthenticatedEndpoint { get; set; } 
	
	[MemberNotNullWhen(true, nameof(AuthenticatedEndpoint))]
	public bool IsAuthenticatedEndpoint => AuthenticatedEndpoint != null;
	// public string? Scope { get; set; }
	public string Route { get; set; } = null!;

	public CacheExpiry? CacheExpiry { get; set; }

	public ESI.Endpoints.RateLimitGroup? RateLimitGroup { get; set; }

	public EsiEndpoint Build()
	{
		Validate();

		return new EsiEndpoint(EndpointId,  Route, HttpMethodType, AuthenticatedEndpoint, CacheExpiry, RateLimitGroup);
	}

	private void Validate()
	{
		if (AuthenticatedEndpoint is not null && !ESI.EsiScopes.Contains(AuthenticatedEndpoint.Scope))
		{
			throw new ArgumentException($"{EndpointId} Endpoint has an invalid scope.", nameof(AuthenticatedEndpoint));
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
