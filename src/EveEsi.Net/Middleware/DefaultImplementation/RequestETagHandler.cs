using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Utilities;
using Microsoft.Extensions.Options;

namespace EveEsi.Net.Middleware;

public class RequestETagHandler(IEtagStorage etagStorage, IOptions<EsiClientConfiguration> options) : IEsiMiddleware
{
	public async Task HandleAsync(EsiRequestContext context, EsiRequestDelegate next,
		CancellationToken cancellationToken = default)
	{
		if (options.Value.EnableETag || context.ExecutionOptions == ExecutionOptions.WithoutETag)
		{
			await next(context);
			return;
		}

		await ApendETagToRequest(context);

		await next(context);

		await StoreETagFromResponse(context);
	}

	protected virtual async Task ApendETagToRequest(EsiRequestContext context)
	{
		string key = GeETagKey(context);
		if (await etagStorage.TryGetETagAsync(key, out string? tag))
		{
			context.Request.Headers.TryAddWithoutValidation("If-None-Match", tag);
		}
	}

	protected virtual async Task StoreETagFromResponse(EsiRequestContext context)
	{
		if (!context.ResponseContext.Response.ResponseMessage.Headers.Contains("ETag"))
		{
			return;
		}

		string etag = context.ResponseContext.Response.Headers.GetValues("ETag").First().Replace("\"", string.Empty);
		string key = GeETagKey(context);
		await etagStorage.StoreEtagAsync(key, etag);
	}


	protected virtual string GeETagKey(EsiRequestContext context)
	{
		return EndpointKeyGenerator.GetKey(context.EndpointId,
			context.Request.Parameters.Route.ToNameValueCollection(),
			context.Request.Parameters.Query.ToNameValueCollection(),
			["datasource"]);
	}
}
