using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using EveEsi.Net.Extensions;

namespace EveEsi.Net.EsiClient;

public class EsiResponse : IDisposable
{
	public List<string> _errors = new();

	public EsiResponse(HttpResponseMessage responseMessage)
	{
		ResponseMessage = responseMessage;
	}

	public HttpResponseMessage ResponseMessage { get; }


	public bool IsSuccessStatusCode => ResponseMessage.IsSuccessStatusCode;

	public HttpStatusCode StatusCode => ResponseMessage.StatusCode;

	public HttpResponseHeaders Headers => ResponseMessage.Headers;

	public void Dispose()
	{
		ResponseMessage.Dispose();
	}

	/// <summary>
	///     Allows the direct assignment of HttpResponseMessage to EsiResponse
	/// </summary>
	/// <param name="response">The Http Response Message</param>
	/// <returns></returns>
	public static implicit operator EsiResponse(HttpResponseMessage response)
	{
		return new EsiResponse(response);
	}

	/// <summary>
	/// Determine if the response is a paged result, and get the total number of pages.
	/// </summary>
	/// <param name="totalPages">The number of pages</param>
	/// <returns></returns>
	public bool IsPagedResponse([NotNullWhen(true)] out long totalPages)
	{
		if (!Headers.Contains("X-Pages"))
		{
			totalPages = -1;
			return false;
		}
		totalPages = long.Parse(Headers.GetValues("X-Pages").First());
		return true;
	}
}

public class EsiResponse<T> : EsiResponse
{
	private T? _cachedData;

	public EsiResponse(HttpResponseMessage responseMessage) : base(responseMessage)
	{
	}

	public bool TryGetData([NotNullWhen(true)] out T? data, [NotNullWhen(false)] out string? errorMessage)
	{
		data = default;
		errorMessage = default;

		if (_cachedData != null)
		{
			data = _cachedData;
			return true;
		}

		if (!IsSuccessStatusCode)
		{
			errorMessage = $"Failed HTTP response. Http status code: {StatusCode}. Error message: {_errors?.First()}";
			return false;
		}

		if (StatusCode == HttpStatusCode.NotModified)
		{
			errorMessage = "No results can be returned because the server returned the NotModified http status code.";
			return false;
		}

		string result = GetStringContent(ResponseMessage.Content);
		try
		{
			if (result.IsPotentiallyJson())
			{
				_cachedData = JsonSerializer.Deserialize<T>(result) ?? throw new JsonException();
			}
			else
			{
				_cachedData = (T)Convert.ChangeType(result, typeof(T));
			}

			data = _cachedData;
			return true;
		}
		catch
		{
			errorMessage =
				$"Failed to deserializa/convert response data. Model type: {typeof(T).Name}; Response data: {result}";
			return false;
		}
	}

	private string GetStringContent(HttpContent httpContent)
	{
		using Stream stream = ResponseMessage.Content.ReadAsStream();
		using StreamReader reader = new(stream);
		return reader.ReadToEnd();
	}
}
