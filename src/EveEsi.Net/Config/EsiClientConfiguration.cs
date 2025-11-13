using EveEsi.Net.Enums.Client;

namespace EveEsi.Net.Config;

public class EsiClientConfiguration
{
	public readonly Dictionary<string, EsiEndpointBuilder> _endpointConfigurations = new(StringComparer.Ordinal);
	private readonly List<EsiEndpointBuilder> endpointBuilders = new();

	public Uri BaseUri { get; set; } = new(ESI.EsiBaseUrl, UriKind.RelativeOrAbsolute);

	public DateOnly CompatibilityDate { get; set; } = ESI.CompatibilityDate;

	public EsiServer EsiService { get; set; } = EsiServer.Tranquility;

	public bool EnableETag { get; set; }

	public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

	public string UserAgent { get; set; } = null!;


	public void AddEndpoint(string endpointId, Action<EsiEndpointBuilder> configureBuilder)
	{
		ArgumentException.ThrowIfNullOrEmpty(endpointId);
		ArgumentNullException.ThrowIfNull(configureBuilder);

		if (_endpointConfigurations.ContainsKey(endpointId))
		{
			return;
		}

		EsiEndpointBuilder builder = new(endpointId);
		configureBuilder(builder);
		_endpointConfigurations.Add(endpointId, builder);
	}

	public EsiEndpoint GetEndpoint(string endpointId)
	{
		if (_endpointConfigurations.TryGetValue(endpointId, out EsiEndpointBuilder? endpointBuilder))
		{
			return endpointBuilder.Build();
		}

		throw new InvalidOperationException($"Endpoint {endpointId} has not been configured");
	}
}
