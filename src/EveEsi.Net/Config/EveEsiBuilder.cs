using EveEsi.Net.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace EveEsi.Net.Config;

public class EveEsiBuilder
{
	internal EveEsiBuilder(IServiceCollection services, EsiClientConfiguration clientConfiguration)
	{
		Services = services;
		EsiClientConfiguration = clientConfiguration;
	}

	public IServiceCollection Services { get; }

	public EsiClientConfiguration EsiClientConfiguration { get; }

	internal MiddlewareCollection MiddlewareCollection { get; } = new();
}
