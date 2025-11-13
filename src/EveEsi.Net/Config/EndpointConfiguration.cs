using Microsoft.Extensions.Options;

namespace EveEsi.Net.Config;

internal class EndpointConfiguration : IPostConfigureOptions<EsiClientConfiguration>
{
	public void PostConfigure(string? name, EsiClientConfiguration options)
	{
		foreach (KeyValuePair<string, Action<EsiEndpointBuilder>> endpoint in ESI.EsiEndpointDefinitions)
		{
			options.AddEndpoint(endpoint.Key, endpoint.Value);
		}
	}
}
