using System.Net;
using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;
using EveEsi.Net.Middleware;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;

namespace EveEsi.Net.UnitTest.RequestClients;

public class EsiHttpClientTests
{
	private EsiHttpClient _client;
	private EsiClientConfiguration _config;
	private IEsiRequestContextFactory _contextFactory;
	private CancellationToken _ct;
	private EsiEndpointBuilder _endpointBuilder;
	private string _endpointId;
	private HttpClient _http;
	private IMiddlewareCollection _mw;
	private IServiceProvider _provider;
	private FakeHttpHandler _underlyingHandler;


	[SetUp]
	public void Setup()
	{
		_endpointId = "test::endpoint";
		_underlyingHandler = new FakeHttpHandler();
		_http = new HttpClient(_underlyingHandler);
		_config = new EsiClientConfiguration();
		_endpointBuilder = new EsiEndpointBuilder(_endpointId)
		{
			Route = "https://test.domain.com/unit/test/", HttpMethodType = HttpMethodType.Get
		};
		_config._endpointConfigurations.Add(_endpointId, _endpointBuilder);

		_contextFactory = Substitute.For<IEsiRequestContextFactory>();
		_provider = Substitute.For<IServiceProvider>();
		_mw = Substitute.For<IMiddlewareCollection>();

		_mw.Build(Arg.Any<EsiRequestDelegate>()).Returns(callInfo =>
		{
			EsiRequestDelegate? terminal = callInfo.Arg<EsiRequestDelegate>();
			return terminal;
		});

		_client = new EsiHttpClient(_http, Options.Create(_config), _contextFactory);
		_ct = new CancellationTokenSource().Token;
	}

	[TearDown]
	public void TearDown()
	{
		_http.Dispose();
		_underlyingHandler.Dispose();
	}

	private EsiRequestContext NewContext(EsiRequest request)
	{
		EsiEndpoint endpoint = _config.GetEndpoint(_endpointId);
		EsiRequestContext ctx = new(_endpointId, endpoint, request, _provider) { CancellationToken = _ct };
		return ctx;
	}

	[Test]
	public async Task ExecuteAsync_PreparesRequest_WithEndpointFromConfig()
	{
		TestRequest request = new();

		EsiRequestContext ctx = NewContext(request);

		_contextFactory.CreateAsync(_endpointId, request, _ct).Returns(ctx);

		bool executed = false;
		_mw.Build(Arg.Any<EsiRequestDelegate>()).Returns(ci =>
		{
			EsiRequestDelegate? temrinal = ci.Arg<EsiRequestDelegate>();
			return async c =>
			{
				executed = true;
				await temrinal(c);
			};
		});


		HttpResponseMessage httpResponse = new(HttpStatusCode.OK);
		_underlyingHandler.OnSend = (_, __) => httpResponse;

		EsiResponseContext result = await _client.ExecuteAsync(_endpointId, request, _mw, _ct);

		executed.ShouldBeTrue();
		_contextFactory.Received(1).CreateAsync(_endpointId, request, _ct);
		ctx.ResponseContext.ShouldBeSameAs(result);
	}


	[Test]
	public async Task ExecuteAsync_PreparesRequest_WithEndpointFromConfiguration()
	{
		TestRequest request = new();

		EsiRequestContext ctx = NewContext(request);
		request.PreparedWith.ShouldBeNull();
		_contextFactory.CreateAsync(_endpointId, request, _ct).Returns(ctx);
		// HTTP returns OK
		HttpResponseMessage httpResponse = new(HttpStatusCode.OK);
		_underlyingHandler.OnSend = (_, __) => httpResponse;
		await _client.ExecuteAsync(_endpointId, request, _mw, _ct);

		request.PreparedWith.ShouldNotBeNull();
	}

	internal sealed class FakeHttpHandler : HttpMessageHandler
	{
		public HttpRequestMessage? LastRequest { get; private set; }
		public CancellationToken LastToken { get; private set; }
		public Func<HttpRequestMessage, CancellationToken, HttpResponseMessage>? OnSend { get; set; }

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			LastRequest = request;
			LastToken = cancellationToken;

			if (OnSend != null)
			{
				return Task.FromResult(OnSend(request, LastToken));
			}

			return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
		}
	}

	internal sealed class TestRequest : EsiRequest
	{
		public EsiEndpoint? PreparedWith { get; set; }

		public override void Prepare(EsiEndpoint endpoint)
		{
			PreparedWith = endpoint;
			base.Prepare(endpoint);
		}
	}
}
