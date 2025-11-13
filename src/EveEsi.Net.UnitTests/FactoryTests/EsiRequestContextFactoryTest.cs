using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;

namespace EveEsi.Net.UnitTest.FactoryTests;

public class EsiRequestContextFactoryTest
{
	private EsiClientConfiguration _config;
	private CancellationToken _ct;
	private EsiEndpointBuilder _endpointBuilder;
	private string _endpointId;

	[SetUp]
	public void Setup()
	{
		_endpointId = "test::endpoint";
		_config = new EsiClientConfiguration();
		_endpointBuilder = new EsiEndpointBuilder(_endpointId)
		{
			Route = "https://test.domain.com/unit/test/", HttpMethodType = HttpMethodType.Get
		};
		_config._endpointConfigurations.Add(_endpointId, _endpointBuilder);
		_ct = new CancellationTokenSource().Token;
	}

	[Test]
	public void RequestContextFactoryTest_CreateRequestContext()
	{
		IServiceProvider? serviceProvider = Substitute.For<IServiceProvider>();
		EsiRequest req = new();
		EsiRequestContextFactory factory = new(serviceProvider, Options.Create(_config));

		EsiRequestContext ctx = factory.CreateAsync(_endpointId, req, _ct);
		ctx.Request.ShouldBeSameAs(req);
		ctx.ScopedServiceProvider.ShouldBeSameAs(serviceProvider);
		ctx.EndpointId.ShouldBe(_endpointId);
		ctx.Endpoint.Route.ShouldBe(_endpointBuilder.Route);
		ctx.Endpoint.MethodType.ShouldBe(HttpMethodType.Get);
	}
}
