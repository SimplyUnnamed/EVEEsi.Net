using EveEsi.Net.Config;
using EveEsi.Net.EsiClient;
using Microsoft.Extensions.Options;

namespace EveEsi.Net.Factories;

internal class EsiRequestContextFactory(
	IServiceProvider serviceProvider,
	IOptions<EsiClientConfiguration> config) : IEsiRequestContextFactory
{
	/// <inheritdoc />
	public EsiRequestContext CreateAsync(string endpointId, EsiRequest request,
		CancellationToken cancellationToken = default)
	{
		EsiEndpoint endpoint = config.Value.GetEndpoint(endpointId);
		return new EsiRequestContext(endpointId, endpoint, request, serviceProvider)
		{
			CancellationToken = cancellationToken
		};
	}
}
