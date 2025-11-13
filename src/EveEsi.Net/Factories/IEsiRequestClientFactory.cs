using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Factories;

internal interface IEsiRequestClientFactory
{
	/// <summary>
	///     Creates a new typed <see cref="EsiRequestClientBase{T}" />
	///     The <see cref="EsiRequestClientBase{T}" /> allows us to augment the middleware pipeline on a per request basis.
	/// </summary>
	/// <param name="endpointId">The Id of the Endpoint to be called</param>
	/// <param name="configure">A configuration call back</param>
	/// <param name="token">Access Token</param>
	/// <typeparam name="TModel">The model representing the response from the esi call</typeparam>
	/// <returns>A new Instance of <see cref="EsiRequestClientBase{T}" /></returns>
	public IRequestClient<TModel> CreateClient<TModel>(string endpointId, Action<RequestParameters> configure,
		string? token = null);

	/// <summary>
	///     Creates a new <see cref="EsiRequestClientBase{T}" />
	///     The <see cref="EsiRequestClientBase{T}" /> allows us to augment the middleware pipeline on a per request basis.
	/// </summary>
	/// <param name="endpointId">The Id of the Endpoint to be called</param>
	/// <param name="configure">A configuration call back</param>
	/// <param name="token">Access Token</param>
	/// <typeparam name="TModel">The model representing the response from the esi call</typeparam>
	/// <returns>A new Instance of <see cref="EsiRequestClientBase{T}" /></returns>
	public IRequestClient CreateClient(string endpointId, Action<RequestParameters> configure, string? token = null);
}
