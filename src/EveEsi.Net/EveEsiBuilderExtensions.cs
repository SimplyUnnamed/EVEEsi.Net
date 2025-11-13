using EveEsi.Net.Config;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Factories;
using EveEsi.Net.Handlers;
using EveEsi.Net.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace EveEsi.Net;

public static class EveEsiBuilderExtensions
{
	public static EveEsiBuilder Use<TMiddleware>(this EveEsiBuilder esiBuilder)
	{
		esiBuilder.Use(typeof(TMiddleware));
		return esiBuilder;
	}

	public static EveEsiBuilder Use(this EveEsiBuilder esiBuilder, Type middleware)
	{
		esiBuilder.Services.AddSingleton(middleware);
		esiBuilder.MiddlewareCollection.AddMiddleware(middleware);
		return esiBuilder;
	}

	public static EveEsiBuilder Use(this EveEsiBuilder esiBuilder,
		Func<EsiRequestContext, EsiRequestDelegate, Task> middleware)
	{
		esiBuilder.MiddlewareCollection.AddMiddleware(middleware);
		return esiBuilder;
	}


	internal static EveEsiBuilder RegisterFactories(this EveEsiBuilder esiBuilder, string httpClientName)
	{
		esiBuilder.Services.AddHttpClient<IEsiHttpClient, EsiHttpClient>(httpClientName, (sp, config) =>
		{
			EsiClientConfiguration defaultOptions = sp.GetRequiredService<IOptions<EsiClientConfiguration>>().Value;
			config.BaseAddress = defaultOptions.BaseUri;
			config.DefaultRequestHeaders.UserAgent.ParseAdd(defaultOptions.UserAgent);
			config.Timeout = defaultOptions.Timeout;
		});

		esiBuilder.Services.AddSingleton<IMiddlewareFactory, MiddlewareFactory>();
		esiBuilder.Services.AddSingleton<IEsiRequestContextFactory, EsiRequestContextFactory>();
		esiBuilder.Services.AddSingleton<IEsiRequestClientFactory, EsiRequestClientFactory>();
		esiBuilder.Services.AddSingleton<IEsiHttpClient>(sp => new EsiHttpClient(
			sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName),
			sp.GetRequiredService<IOptions<EsiClientConfiguration>>(),
			sp.GetRequiredService<IEsiRequestContextFactory>())
		);

		esiBuilder.Services.AddSingleton<IEsiGlobalMiddlewareCollection>(_ =>
		{
			esiBuilder.MiddlewareCollection.Freeze();
			return esiBuilder.MiddlewareCollection;
		});

		return esiBuilder;
	}

	internal static EveEsiBuilder RegisterRoutes(this EveEsiBuilder esiBuilder)
	{
		esiBuilder.Services.TryAddEnumerable(ServiceDescriptor
			.Singleton<IPostConfigureOptions<EsiClientConfiguration>, EndpointConfiguration>());
		return esiBuilder;
	}

	internal static EveEsiBuilder RegisterEsiClients(this EveEsiBuilder esiBuilder)
	{
		esiBuilder.Services.AddSingleton<IAllianceEndpoints, AllianceEndpoints>();
		esiBuilder.Services.AddSingleton<IAssetsEndpoints, AssetEndpoints>();
		esiBuilder.Services.AddSingleton<ICalendarEndpoints, CalendarEndpoints>();
		esiBuilder.Services.AddSingleton<ICharacterEndpoints, CharacterEndpoints>();
		esiBuilder.Services.AddSingleton<IContractEndpoints, ContractEndpoints>();


		return esiBuilder;
	}
}
