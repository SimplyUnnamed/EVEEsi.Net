using System.Collections.Concurrent;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using EveEsi.Net.Config;
using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Services;

namespace EveEsi.Net.Middleware;

public class ScopeAccessMiddleware(ITokenClaimsParser claimsParser) : IEsiMiddleware
{

	private void TestLong(long page)
	{
		
	}
	public Task HandleAsync(EsiRequestContext context, EsiRequestDelegate next,
		CancellationToken cancellationToken = default)
	{
		if (context.Endpoint.AuthenticatedEndpoint is null)
		{
			return next(context);
		}

		EsiEndpoint endpoint = context.Endpoint;
		string? token = context.Request.Token;

		if (string.IsNullOrWhiteSpace(endpoint.AuthenticatedEndpoint.Scope))
		{
			throw new InvalidOperationException("Protected endpoint must specify a scope");
		}

		if (string.IsNullOrWhiteSpace(token))
		{
			ProduceFailureResponse(context, "Protected endpoint must have a valid access token");
			return Task.CompletedTask;
		}

		if (!TokenHasScope(token, endpoint.AuthenticatedEndpoint.Scope))
		{
			ProduceFailureResponse(context, $"Provided access token does not have required scope '{endpoint.AuthenticatedEndpoint.Scope}'");
			return Task.CompletedTask;
		}

		return next(context);
	}

	private bool TokenHasScope(string token, string scope)
	{
		ClaimsPrincipal principal = claimsParser.ParseToken(token);
		string[] tokenScopes = principal.Claims.Where(x => x.Type == "scp").Select(x => x.Value).ToArray();
		return tokenScopes.Contains(scope);
	}


	private void ProduceFailureResponse(EsiRequestContext context, string errorMessage)
	{
		context.ResponseContext.Response = new EsiResponse(new HttpResponseMessage(HttpStatusCode.Forbidden)
		{
			Content = new StringContent(JsonSerializer.Serialize(new { error = errorMessage }), Encoding.UTF8,
				"application/json")
		});
	}
}
