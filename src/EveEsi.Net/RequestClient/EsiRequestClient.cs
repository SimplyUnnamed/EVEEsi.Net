using EveEsi.Net.Extensions;
using EveEsi.Net.Middleware;

namespace EveEsi.Net.EsiClient;

/// <summary>
///     The base ESI Request client taking care of per request middleware.
/// </summary>
/// <typeparam name="TResponse">The Response object expected</typeparam>
internal abstract class EsiRequestBaseClient<TResponse> : IEsiRequestClient<TResponse>
{
	protected readonly IEsiHttpClient _client;
	protected readonly string _endpointId;
	protected readonly EsiRequest _esiRequest;
	protected readonly IMiddlewareCollection _middleware;

	public EsiRequestBaseClient(IEsiHttpClient client, IMiddlewareCollection middleware, EsiRequest esiRequest,
		string endpointId)
	{
		_client = client;
		_middleware = middleware;
		_esiRequest = esiRequest;
		_endpointId = endpointId;
	}

	public IEsiRequestClient<TResponse> AddMiddleware<TMiddleware>()
	{
		_middleware.AddMiddleware<TMiddleware>();
		return this;
	}

	public IEsiRequestClient<TResponse> AddMiddleware(Type type)
	{
		_middleware.AddMiddleware(type);
		return this;
	}

	public IEsiRequestClient<TResponse> AddMiddleware(Func<EsiRequestContext, EsiRequestDelegate, Task> middleware)
	{
		_middleware.AddMiddleware(middleware);
		return this;
	}

	public abstract Task<TResponse> ExecuteAsync(CancellationToken cancellationToken = default);
}

/// <summary>
///     A Request client which does not expect a response body
/// </summary>
internal class EsiRequestClient : EsiRequestBaseClient<EsiResponse>, IRequestClient
{
	public EsiRequestClient(IEsiHttpClient client, IMiddlewareCollection middlewareCollection, EsiRequest request,
		string endpointId) : base(client, middlewareCollection, request, endpointId)
	{
	}

	/// <summary>
	///     Executes the esi client call, and returns the response object.
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public override async Task<EsiResponse> ExecuteAsync(CancellationToken cancellationToken = default)
	{
		_middleware.Freeze();
		EsiResponseContext ctx = await _client.ExecuteAsync(_endpointId, _esiRequest, _middleware, cancellationToken);
		return ctx.Response;
	}
}

/// <summary>
///     An ESI Request Client which expects a model in its response body
/// </summary>
/// <typeparam name="TModel">The Expected model to be returned from the ESI request</typeparam>
internal class EsiRequestClient<TModel> : EsiRequestBaseClient<EsiResponse<TModel>>, IRequestClient<TModel>
{
	public EsiRequestClient(IEsiHttpClient client, IMiddlewareCollection middleware, EsiRequest esiRequest,
		string endpointId) : base(client, middleware, esiRequest, endpointId)
	{
	}

	/// <summary>
	///     Executes the http request, and wraps the response object in a typed Response object.
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public override async Task<EsiResponse<TModel>> ExecuteAsync(CancellationToken cancellationToken = default)
	{
		_middleware.Freeze();
		EsiResponseContext response =
			await _client.ExecuteAsync(_endpointId, _esiRequest, _middleware, cancellationToken);
		return new EsiResponse<TModel>(response.Response.ResponseMessage);
	}
}
