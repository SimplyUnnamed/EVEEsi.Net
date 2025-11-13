using EveEsi.Net.Middleware;

namespace EveEsi.Net.Factories;

internal interface IMiddlewareFactory
{
	public IEsiMiddleware? Create(Type middleware);
}
