namespace EveEsi.Net.EsiClient;

public class EsiResponseContext : IDisposable
{
	/// <summary>
	///     A Response context for an ESI Request
	/// </summary>
	/// <param name="requestContext">The request context</param>
	public EsiResponseContext(EsiRequestContext requestContext)
	{
		RequestContext = requestContext;
	}

	/// <summary>
	///     See Responses Request at <see cref="EsiRequestContext" />
	/// </summary>
	public EsiRequestContext RequestContext { get; }


	/// <summary>
	///     The Http Response message from the HttpClient
	/// </summary>
	private EsiResponse? _response { get; set; }

	public EsiResponse Response
	{
		get
		{
			if (_response == null)
			{
				throw new InvalidOperationException("Cannot access the ESI Response before it has been set.");
			}

			return _response;
		}
		set => _response = value;
	}


	public bool IsSuccessStatusCode => Response.IsSuccessStatusCode;

	public void Dispose()
	{
		if (_response != null)
		{
			_response.Dispose();
		}
	}
}

public class EsiResponseContext<T> : EsiResponseContext
{
	public EsiResponseContext(EsiResponseContext ctx) : base(ctx.RequestContext)
	{
		Response = ctx.Response;
	}
}
