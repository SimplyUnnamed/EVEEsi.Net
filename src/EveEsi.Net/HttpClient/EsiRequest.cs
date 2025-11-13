using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;

namespace EveEsi.Net.EsiClient;

public class EsiRequest : HttpRequestMessage
{
	public EsiRequest(Action<RequestParameters> configure)
	{
		configure(Parameters);
	}

	public EsiRequest()
	{
	}

	/// <summary>
	///     If required, an access token to send with the Http Request
	/// </summary>
	public string? Token { get; set; }


	public RequestParameters Parameters { get; } = new();


	[StringSyntax(StringSyntaxAttribute.Uri)]
	public string? RequestUrl
	{
		get => RequestUri?.ToString();
		set => RequestUri = new Uri(value!, UriKind.RelativeOrAbsolute);
	}


	public virtual void Prepare(EsiEndpoint endpoint)
	{
		switch (endpoint.MethodType)
		{
			case HttpMethodType.Get: Method = HttpMethod.Get; break;
			case HttpMethodType.Post: Method = HttpMethod.Post; break;
			case HttpMethodType.Put: Method = HttpMethod.Put; break;
			case HttpMethodType.Delete: Method = HttpMethod.Delete; break;
			default:
				throw new NotImplementedException(endpoint.MethodType.ToString());
		}

		SetupAuthorizationHeader(endpoint);
		SetupRequestBody(Parameters.Body);

		SetupRequestUri(endpoint);
	}

	private void SetupAuthorizationHeader(EsiEndpoint endpoint)
	{
		if (!endpoint.ProtectedEndpoint)
		{
			return;
		}

		if (string.IsNullOrEmpty(Token) && Headers.Authorization is null)
		{
			throw new InvalidOperationException(
				"Request prepared against protected endpoint without an authentication token");
		}

		Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
	}

	private void SetupRequestBody(object? body)
	{
		if (body is null)
		{
			return;
		}

		if (Content is not null)
		{
			throw new InvalidOperationException("Request body has already been set.");
		}

		Content = new StringContent(JsonSerializer.Serialize(body));
	}

	private void SetupRequestUri(EsiEndpoint endpoint)
	{
		RequestUrl = BuildUrl(endpoint.Route, Parameters.Route, Parameters.Query);

		string BuildUrl(string template, RequestParameterCollection routeParameters,
			RequestParameterCollection queryParameters)
		{
			NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
			StringBuilder path = new(template);

			foreach (KeyValuePair<string, string> kvp in routeParameters)
			{
				path = path.Replace($"[{kvp.Key}]", kvp.Value);
			}

			foreach (KeyValuePair<string, string> kvp in queryParameters)
			{
				if (!string.IsNullOrEmpty(kvp.Value))
				{
					query[kvp.Key] = kvp.Value;
				}
			}


			return query.Count > 0 ? string.Concat(path.ToString(), "?", query.ToString()) : path.ToString();
		}
	}
}
