using EveEsi.Net.EsiClient;
using EveEsi.Net.Middleware;

namespace EveEsi.Net.Factories;

internal class EsiRequestClientFactory(
	IEsiHttpClient httpClient,
	IEsiGlobalMiddlewareCollection globalMiddleware) : IEsiRequestClientFactory
{
	/// <inheritdoc />
	public IRequestClient<TModel> CreateClient<TModel>(string endpointId, Action<RequestParameters> configure,
		string? token = null)
	{
		EsiRequest request = new(configure);
		request.Token = token;

		return new EsiRequestClient<TModel>(
			httpClient,
			GetMiddlewareCollection(),
			request,
			endpointId);
	}

	/// <inheritdoc />
	public IRequestClient CreateClient(string endpointId, Action<RequestParameters> configure, string? token = null)
	{
		EsiRequest request = new(configure);
		request.Token = token;
		return new EsiRequestClient(
			httpClient,
			GetMiddlewareCollection(),
			request,
			endpointId);
	}

	/// <summary>
	///     Gets the list of registered global middleware, and clones it to use in the request client.
	/// </summary>
	/// <returns>A Cloned copy of the middleware pipeline</returns>
	private IMiddlewareCollection GetMiddlewareCollection()
	{
		return globalMiddleware.Clone();
	}
}
