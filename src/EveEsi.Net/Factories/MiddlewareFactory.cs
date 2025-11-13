using EveEsi.Net.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace EveEsi.Net.Factories;

internal class MiddlewareFactory(IServiceProvider serviceProvider) : IMiddlewareFactory
{
	public IEsiMiddleware? Create(Type middleware)
	{
		return (IEsiMiddleware?)ActivatorUtilities.CreateInstance(serviceProvider, middleware);
	}
}
