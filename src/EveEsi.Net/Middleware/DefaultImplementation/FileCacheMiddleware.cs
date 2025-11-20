using System.Net;
using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Middleware;

public class FileCacheMiddleware(IEsiCacheService cacheService) : IEsiMiddleware
{
	public async Task HandleAsync(EsiRequestContext context, EsiRequestDelegate next,
		CancellationToken cancellationToken = default)
	{
		if (context.Endpoint.MethodType != HttpMethodType.Get || context.Endpoint.CacheExpiry == null)
		{
			await next(context);
			return;
		}


		EsiRequest request = context.Request;
		string keyRoute = EndpointKeyGenerator.GenerateRoute(
			context.EndpointId,
			request.Parameters.Route.ToNameValueCollection(),
			request.Parameters.Query.ToNameValueCollection());

		HttpResponseCache? cachedItem = await cacheService.GetValue<HttpResponseCache>(keyRoute);
		if (cachedItem != null)
		{
			ApplyCachedResponse(context, cachedItem);
			return;
		}

		await next(context);

		if (!context.ResponseContext.IsSuccessStatusCode)
		{
			return;
		}

		cachedItem = new HttpResponseCache
		{
			StatusCode = context.ResponseContext.Response.ResponseMessage.StatusCode,
			Content =
				await context.ResponseContext.Response.ResponseMessage.Content.ReadAsByteArrayAsync(
					cancellationToken),
			Headers = context.ResponseContext.Response.Headers.ToDictionary(x => x.Key, x => x.Value)
		};

		TimeSpan ttl = context.Endpoint.CacheExpiry.CalculateExpiry();
		await cacheService.StoreValue(keyRoute, cachedItem, ttl);
	}


	private void ApplyCachedResponse(EsiRequestContext context, HttpResponseCache responseCache)
	{
		context.ResponseContext.Response = new HttpResponseMessage(responseCache.StatusCode)
		{
			Content = new ByteArrayContent(responseCache.Content)
		};

		foreach (KeyValuePair<string, IEnumerable<string>> header in responseCache.Headers)
		{
			context.ResponseContext.Response.Headers.Add(header.Key, header.Value);
		}

		context.ResponseContext.Response.Headers.Add("X-EsiNet-Cache", "HIT");
	}

	private sealed class HttpResponseCache
	{
		public HttpStatusCode StatusCode { get; set; }
		public Dictionary<string, IEnumerable<string>> Headers { get; set; } = new();
		public byte[] Content { get; set; } = [];
	}
}
