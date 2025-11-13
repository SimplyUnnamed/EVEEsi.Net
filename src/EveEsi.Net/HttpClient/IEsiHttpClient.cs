using EveEsi.Net.Middleware;

namespace EveEsi.Net.EsiClient;

public delegate Task EsiRequestDelegate(EsiRequestContext ctx);

internal interface IEsiHttpClient
{
	/// <summary>
	///     Configures the ESI Request pipeline and Executes it
	/// </summary>
	/// <param name="endpointId">The ID of the ESI Endpoint</param>
	/// <param name="request">The Esi Request Object</param>
	/// <param name="middleware">The Configured Middleware Collection</param>
	/// <param name="cancellationToken">The Cancellation Token</param>
	/// <returns>The Response Context of the ESI Request</returns>
	Task<EsiResponseContext> ExecuteAsync(string endpointId, EsiRequest request, IMiddlewareCollection middleware,
		CancellationToken cancellationToken = default);
}
