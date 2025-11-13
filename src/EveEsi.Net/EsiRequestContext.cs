using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;

namespace EveEsi.Net.EsiClient;

/// <summary>
///     The Request Context for an ESI Call
/// </summary>
public class EsiRequestContext
{
	public EsiRequestContext(string endpointId, EsiEndpoint endpoint, EsiRequest request,
		IServiceProvider serviceProvider)
	{
		EndpointId = endpointId;
		Request = request;
		Endpoint = endpoint;
		ScopedServiceProvider = serviceProvider;
		ResponseContext = new EsiResponseContext(this);
	}

	/// <summary>
	///     The Endpoint Id being called
	/// </summary>
	public string EndpointId { get; }

	public EsiEndpoint Endpoint { get; }

	/// <summary>
	///     Execurtion options
	/// </summary>
	public ExecutionOptions ExecutionOptions { get; set; } = ExecutionOptions.None;

	/// <summary>
	///     The Cancellation Token
	/// </summary>
	public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

	/// <summary>
	///     Scoped Service Provider
	/// </summary>
	public IServiceProvider ScopedServiceProvider { get; }

	/// <summary>
	///     The request being submitted to the ESI Call
	/// </summary>
	public EsiRequest Request { get; }

	/// <summary>
	///     The response context
	/// </summary>
	public EsiResponseContext ResponseContext { get; }

	/// <summary>
	///     Additional Properties for the context.
	/// </summary>
	public Dictionary<string, object> Properties { get; } = new();
}
