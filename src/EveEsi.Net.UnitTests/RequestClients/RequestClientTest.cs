using System.Net;
using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Middleware;
using NSubstitute;
using Shouldly;

namespace EveEsi.Net.UnitTest.RequestClients;

internal class RequestClientTest
{
	private CancellationToken _ct;
	private string _endpoint;
	private IEsiHttpClient _http;
	private IMiddlewareCollection _mw;
	private IServiceProvider _provider;
	private EsiRequest _req;

	[SetUp]
	public void SetUp()
	{
		_http = Substitute.For<IEsiHttpClient>();
		_mw = Substitute.For<IMiddlewareCollection>();
		_req = new EsiRequest();
		_endpoint = "get:/characters/{id}";
		_ct = new CancellationTokenSource().Token;
		_provider = Substitute.For<IServiceProvider>();
	}

	[TearDown]
	public void TearDown()
	{
		_req.Dispose();
	}

	private EsiResponseContext BuildResponseContext()
	{
		EsiEndpoint testEndpoint = new(_endpoint, "/unit/test", HttpMethodType.Get, null, null, null);
		EsiRequestContext context = new(_endpoint, testEndpoint, _req, _provider);
		context.ResponseContext.Response = new EsiResponse(new HttpResponseMessage(HttpStatusCode.OK));
		return context.ResponseContext;
	}


	[Test]
	public void EsiClient_AddMiddlewareByGeneric_AddsMiddlewareDelegate_AndReturnsSelf()
	{
		EsiRequestClient sut = new(_http, _mw, _req, _endpoint);
		IEsiRequestClient<EsiResponse> returned = sut.AddMiddleware<DummyMiddleware>();

		_mw.Received(1).AddMiddleware(Arg.Any<MiddlewareComponent>());
		returned.ShouldBeSameAs(sut);
	}

	[Test]
	public void EsiClient_AddMiddlewareByType_AddsMiddlewareDelegate_AndReturnsSelf()
	{
		EsiRequestClient sut = new(_http, _mw, _req, _endpoint);
		IEsiRequestClient<EsiResponse> returned = sut.AddMiddleware(typeof(DummyMiddleware));

		_mw.Received(1).AddMiddleware(Arg.Any<MiddlewareComponent>());
		returned.ShouldBeSameAs(sut);
	}

	[Test]
	public void EsiClient_AddMiddlewareDelegate_AddsMiddlewareDelegate_AndReturnsSelf()
	{
		EsiRequestClient sut = new(_http, _mw, _req, _endpoint);
		IEsiRequestClient<EsiResponse> returned = sut.AddMiddleware((ctx, next) => { return next(ctx); });

		_mw.Received(1).AddMiddleware(Arg.Any<MiddlewareComponent>());
		returned.ShouldBeSameAs(sut);
	}

	[Test]
	public async Task
		EsiClient_WhenExecuteIsCalled_FreezeMiddlewareCollection_And_CallsHttpClient_AndReturnsEsiResponseContext()
	{
		EsiRequestClient sut = new(_http, _mw, _req, _endpoint);
		_http.ExecuteAsync(Arg.Any<string>(), Arg.Any<EsiRequest>(), Arg.Any<IMiddlewareCollection>(),
			Arg.Any<CancellationToken>()).Returns(BuildResponseContext());

		EsiResponse result = await sut.ExecuteAsync(_ct);

		await _http.Received(1).ExecuteAsync(Arg.Is(_endpoint), Arg.Is(_req), Arg.Is(_mw), Arg.Is(_ct));
		_mw.Received(1).Freeze();
		result.ShouldNotBeNull();
		result.ShouldBeOfType<EsiResponse>();
	}

	[Test]
	public void EsiClientOfT_AddMiddlewareByGeneric_AddsMiddlewareDelegate_AndReturnsSelf()
	{
		EsiRequestClient<FakeDto> sut = new(_http, _mw, _req, _endpoint);
		IEsiRequestClient<EsiResponse<FakeDto>> returned = sut.AddMiddleware<DummyMiddleware>();

		_mw.Received(1).AddMiddleware(Arg.Any<MiddlewareComponent>());
		returned.ShouldBeSameAs(sut);
	}

	[Test]
	public void EsiClientOfT_AddMiddlewareByType_AddsMiddlewareDelegate_AndReturnsSelf()
	{
		EsiRequestClient<FakeDto> sut = new(_http, _mw, _req, _endpoint);
		IEsiRequestClient<EsiResponse<FakeDto>> returned = sut.AddMiddleware(typeof(DummyMiddleware));

		_mw.Received(1).AddMiddleware(Arg.Any<MiddlewareComponent>());
		returned.ShouldBeSameAs(sut);
	}

	[Test]
	public void EsiClientOfT_AddMiddlewareDelegate_AddsMiddlewareDelegate_AndReturnsSelf()
	{
		EsiRequestClient<FakeDto> sut = new(_http, _mw, _req, _endpoint);
		IEsiRequestClient<EsiResponse<FakeDto>> returned = sut.AddMiddleware((ctx, next) =>
		{
			return next(ctx);
		});

		_mw.Received(1).AddMiddleware(Arg.Any<MiddlewareComponent>());
		returned.ShouldBeSameAs(sut);
	}

	[Test]
	public async Task
		EsiClientOfT_WhenExecuteIsCalled_FreezeMiddlewareCollection_And_CallsHttpClient_AndReturnsEsiResponseContextOfT()
	{
		EsiRequestClient<FakeDto> sut = new(_http, _mw, _req, _endpoint);
		_http.ExecuteAsync(Arg.Any<string>(), Arg.Any<EsiRequest>(), Arg.Any<IMiddlewareCollection>(),
			Arg.Any<CancellationToken>()).Returns(BuildResponseContext());
		EsiResponse<FakeDto> result = await sut.ExecuteAsync(_ct);

		await _http.Received(1).ExecuteAsync(Arg.Is(_endpoint), Arg.Is(_req), Arg.Is(_mw), Arg.Is(_ct));
		_mw.Received(1).Freeze();
		result.ShouldNotBeNull();
		result.ShouldBeOfType<EsiResponse<FakeDto>>();
	}


	private sealed class DummyMiddleware : IEsiMiddleware
	{
		public Task HandleAsync(EsiRequestContext context, EsiRequestDelegate next,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}

	private sealed record FakeDto(int Id = 1);
}
