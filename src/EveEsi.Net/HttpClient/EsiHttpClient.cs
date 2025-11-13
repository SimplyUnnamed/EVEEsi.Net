using EveEsi.Net.Config;
using EveEsi.Net.Factories;
using EveEsi.Net.Middleware;
using Microsoft.Extensions.Options;

namespace EveEsi.Net.EsiClient;

internal class EsiHttpClient : IEsiHttpClient
{
	private readonly EsiClientConfiguration _configuration;
	private readonly HttpClient _httpClient;
	private readonly IEsiRequestContextFactory _requestContextFactory;

	/// <summary>
	///     Esi Client to configure and run the ESI Request
	/// </summary>
	/// <param name="httpClient">the HttpClient to use</param>
	/// <param name="configuration">The Esi Client Configuration</param>
	/// <param name="requestContextFactory">Context factory to create the request context</param>
	public EsiHttpClient(
		HttpClient httpClient,
		IOptions<EsiClientConfiguration> configuration,
		IEsiRequestContextFactory requestContextFactory)
	{
		_httpClient = httpClient;
		_configuration = configuration.Value;
		_requestContextFactory = requestContextFactory;
	}

	/// <inheritdoc />
	public async Task<EsiResponseContext> ExecuteAsync(
		string endpointId,
		EsiRequest request,
		IMiddlewareCollection middleware,
		CancellationToken cancellationToken = default)
	{
		EsiRequestContext ctx = _requestContextFactory.CreateAsync(endpointId, request, cancellationToken);
		EsiRequestDelegate terminal = BuildRequestDelegate();
		terminal = middleware.Build(terminal);
		await terminal(ctx);
		return ctx.ResponseContext;
	}

	/// <summary>
	///     Builder the terminating Request Delegate which makes the http call
	/// </summary>
	/// <returns>A request Delegate to run</returns>
	private EsiRequestDelegate BuildRequestDelegate()
	{
		EsiRequestDelegate terminal = async ctx =>
		{
			ctx.Request.Prepare(ctx.Endpoint);
			HttpResponseMessage response = await _httpClient.SendAsync(ctx.Request, ctx.CancellationToken);
			ctx.ResponseContext.Response = response;
		};
		return terminal;
	}
}
