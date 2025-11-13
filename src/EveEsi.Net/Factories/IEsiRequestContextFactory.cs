using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Factories;

internal interface IEsiRequestContextFactory
{
	/// <summary>
	///     Creates a EsiRequestContextFactory
	/// </summary>
	/// <param name="endpointId">the Id of the ESI Endpoint being called</param>
	/// <param name="request">The Request Object</param>
	/// <param name="cancellationToken">The Cancellation token to use for the pipline</param>
	/// <returns>A new EsiRequestContext object.</returns>
	public EsiRequestContext CreateAsync(string endpointId, EsiRequest request,
		CancellationToken cancellationToken = default);
}
