namespace EveEsi.Net.Middleware;

internal interface IEsiGlobalMiddlewareCollection
{
	/// <summary>
	///     Clones the middleware collection
	/// </summary>
	/// <returns></returns>
	public IMiddlewareCollection Clone();
}
