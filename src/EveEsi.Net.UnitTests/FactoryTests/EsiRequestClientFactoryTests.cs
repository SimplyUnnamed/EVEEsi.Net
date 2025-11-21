using System.Net;
using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;
using EveEsi.Net.Middleware;
using NSubstitute;
using Shouldly;

namespace EveEsi.Net.UnitTest.FactoryTests;

public class EsiRequestClientFactoryTests
{
	private IMiddlewareCollection _clone = null!;
	private IEsiGlobalMiddlewareCollection _global = null!;


	private IEsiHttpClient _http = null!;
	private IServiceProvider _services = null!;
	private EsiRequestClientFactory _sut = null!;

	[SetUp]
	public void Setup()
	{
		_http = Substitute.For<IEsiHttpClient>();
		_global = Substitute.For<IEsiGlobalMiddlewareCollection>();
		_clone = Substitute.For<IMiddlewareCollection>();
		_services = Substitute.For<IServiceProvider>();
		_global.Clone().Returns(_clone);
		_sut = new EsiRequestClientFactory(_http, _global);

		_http.ExecuteAsync(
				Arg.Any<string>(),
				Arg.Any<EsiRequest>(),
				Arg.Any<IMiddlewareCollection>(),
				Arg.Any<CancellationToken>())
			.Returns(ci =>
			{
				EsiEndpoint endpoint = new(ci.Arg<string>(), "/unit/test", HttpMethodType.Get, null, null, null);
				EsiRequestContext requestContext = new(ci.Arg<string>(), endpoint, ci.Arg<EsiRequest>(), _services);
				requestContext.ResponseContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
				return requestContext.ResponseContext;
			});
	}

	[Test]
	public async Task CreateClient_WhenTokenIsProvided_RequestHasTokenSet()
	{
		// Arrange
		string endpoint = "universe/ids";
		string token = "random_auth_token";
		CancellationToken ct = new();


		// Act
		IRequestClient client = _sut.CreateClient(endpoint, p => { }, token);
		await client.ExecuteAsync(ct);

		// Assert
		_global.Received(1).Clone();

		await _http.Received(1).ExecuteAsync(
			Arg.Is(endpoint),
			Arg.Is<EsiRequest>(r => r.Token == token),
			Arg.Is(_clone),
			Arg.Is(ct));
	}

	[Test]
	public async Task CreateClient_UsesClonedMiddleware_AndExecutesViaHttpClient()
	{
		// Arrange
		string endpoint = "universe/ids";
		CancellationToken ct = new();


		// Act
		IRequestClient client = _sut.CreateClient(endpoint, p => { });
		await client.ExecuteAsync(ct);

		// Assert
		_global.Received(1).Clone();

		await _http.Received(1).ExecuteAsync(
			Arg.Is(endpoint),
			Arg.Is<EsiRequest>(r => r.Token == null),
			Arg.Is(_clone),
			Arg.Is(ct));
	}

	[Test]
	public async Task CreateClientOfT_WhenTokenIsProvided_RequestHasTokenSet()
	{
		// Arrange
		string endpoint = "universe/ids";
		string token = "random_auth_token";
		CancellationToken ct = new();


		// Act
		IRequestClient<FakeDto> client = _sut.CreateClient<FakeDto>(endpoint, p => { }, token);
		client.ShouldBeAssignableTo<IRequestClient<FakeDto>>();
		await client.ExecuteAsync(ct);

		// Assert
		_global.Received(1).Clone();

		await _http.Received(1).ExecuteAsync(
			Arg.Is(endpoint),
			Arg.Is<EsiRequest>(r => r.Token == token),
			Arg.Is(_clone),
			Arg.Is(ct));
	}

	[Test]
	public async Task CreateClientOfT_UsesClonedMiddleware_AndExecutesViaHttpClient()
	{
		// Arrange
		string endpoint = "universe/ids";
		CancellationToken ct = new();


		// Act
		IRequestClient<FakeDto> client = _sut.CreateClient<FakeDto>(endpoint, p => { });
		client.ShouldBeAssignableTo<IRequestClient<FakeDto>>();
		await client.ExecuteAsync(ct);

		// Assert
		_global.Received(1).Clone();

		await _http.Received(1).ExecuteAsync(
			Arg.Is(endpoint),
			Arg.Is<EsiRequest>(r => r.Token == null),
			Arg.Is(_clone),
			Arg.Is(ct));
	}


	private record FakeDto(int Value);
}
