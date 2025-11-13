using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Middleware;

internal interface IMiddlewareCollection
{
	/// <summary>
	///     The Middleware pipeline
	/// </summary>
	IReadOnlyList<MiddlewareComponent> Middleware { get; }


	/// <summary>
	///     Adds middleware into the pipeline
	/// </summary>
	/// <param name="middleware"></param>
	/// <returns></returns>
	public IMiddlewareCollection AddMiddleware(MiddlewareComponent middleware);

	/// <summary>
	///     Adds to the middleware Collection
	/// </summary>
	/// <param name="terminal">The middleware request delegate</param>
	internal EsiRequestDelegate Build(EsiRequestDelegate terminal);

	internal void Freeze();
}
