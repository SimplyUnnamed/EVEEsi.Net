using System.Text;
using EveEsi.Net.Config;
using EveEsi.Net.Middleware;
using EveEsi.Net.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EveEsi.Net;

public static class DependencyInjection
{
	public static EveEsiBuilder ConfigureEveEsiClient(this IServiceCollection services, string name,
		string version = "1.0.0")
	{
		return ConfigureEveEsiClient(services, esi =>
		{
		}, name, version);
	}

	public static EveEsiBuilder ConfigureEveEsiClient(this IServiceCollection services,
		Action<EsiClientConfiguration> configure,
		string name,
		string version = "1.0.0")
	{
		EsiClientConfiguration config = new();

		configure(config);
		config.UserAgent = BuildUserAgentName(name, version);
		services.Configure<EsiClientConfiguration>(c =>
		{
			configure(c);
			c.UserAgent = BuildUserAgentName(name, version);
		});

		EveEsiBuilder builder = new(services, config);

		builder.Services.AddSingleton<IEsiCacheService, FileResponseCache>();
		builder.Services.AddSingleton<IEtagStorage, ETagStorage>();
		builder.Use<RequestETagHandler>();
		builder.Use<FileCacheMiddleware>();

		builder.RegisterFactories($"EsiClient.Net:{name}:{version}")
			.RegisterEsiClients()
			.RegisterRoutes();

		builder.Use(async (ctx, next) =>
		{
			var logger = ctx.ScopedServiceProvider.GetRequiredService<ILogger>();
			await next(ctx);
		});
		
		return builder;
	}


	private static string BuildUserAgentName(string name, string version = "1.0.0")
	{
		StringBuilder sb = new(256);
		sb.Append($"EsiClient/{ESI.Version} ");
		sb.Append($"(.NET {Environment.Version}; +{ESI.GithubAddress}) ");
		sb.Append($"{name.Trim()}/{version.Trim()}");

		return sb.Length > 256 ? sb.ToString(0, 256) : sb.ToString();
	}
}
