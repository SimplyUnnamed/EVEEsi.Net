using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Middleware;

/// <summary>
///     Class stores a middleware implementation
/// </summary>
internal sealed class MiddlewareComponent
{
	public MiddlewareComponent(Func<EsiRequestDelegate, EsiRequestDelegate> middleware, int priority)
	{
		Middleware = middleware;
		Priority = priority;
	}

	/// <summary>
	///     The Middleware Function
	/// </summary>
	public Func<EsiRequestDelegate, EsiRequestDelegate> Middleware { get; }

	/// <summary>
	///     The middleware execution priority
	/// </summary>
	public int Priority { get; }
}
