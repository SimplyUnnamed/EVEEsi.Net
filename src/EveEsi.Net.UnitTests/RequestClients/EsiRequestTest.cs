using System.Text;
using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using Shouldly;

namespace EveEsi.Net.UnitTest.RequestClients;

public class EsiRequestTest
{
	public static EsiRequestTestCase[] EndpointTests =
	{
		new(new EsiEndpointBuilder("rq::1") { Route = "/unit/[testId]/test" }, p =>
		{
			p.Route["testId"] = "4444";
		}, "/unit/4444/test"),
		new(new EsiEndpointBuilder("endpointId") { Route = "/unit/test" }, p =>
		{
			p.Route["testId"] = "4444";
		}, "/unit/test"),
		new(new EsiEndpointBuilder("endpointId") { Route = "/unit/test" }, p =>
		{
			p.Query["testId"] = "4444";
		}, "/unit/test?testId=4444"),
		new(new EsiEndpointBuilder("endpointId") { Route = "/unit/[testId]/test" }, p =>
		{
			p.Route["testId"] = "4444";
			p.Query["queryId"] = "4444";
			p.Query["itemId"] = "20049";
		}, "/unit/4444/test?queryId=4444&itemId=20049")
	};

	public static EsiRequestTestCase[] EndpointBodyTests =
	{
		new(
			new EsiEndpointBuilder("endpointId")
			{
				Route = "/unit/[testId]/test", HttpMethodType = HttpMethodType.Post
			}, p =>
			{
				p.Route["testId"] = "4444";
				p.Body = new[] { 1123, 4424, 55565 };
			}, "[1123,4424,55565]"),
		new(
			new EsiEndpointBuilder("endpointId")
			{
				Route = "/unit/[testId]/test", HttpMethodType = HttpMethodType.Post
			}, p =>
			{
				p.Route["testId"] = "4444";
				p.Body = new { ItemValue = "3393isk", ItemId = 339493 };
			}, "{\"ItemValue\":\"3393isk\",\"ItemId\":339493}"),
		new(
			new EsiEndpointBuilder("endpointId")
			{
				Route = "/unit/{testId}/test", HttpMethodType = HttpMethodType.Post
			}, p =>
			{
				p.Route["testId"] = "4444";
				p.Body = new Dictionary<string, string> { { "ItemValue", "3393isk" }, { "ItemId", "339493" } };
			}, "{\"ItemValue\":\"3393isk\",\"ItemId\":\"339493\"}")
	};


	[TestCaseSource(nameof(EndpointTests))]
	public void EsiRequest_RouteIsPrepared_FromEndpointAndParameterConfig(EsiRequestTestCase esiRequestTestCase)
	{
		EsiRequest request = new(esiRequestTestCase.ParameterConfig);
		EsiEndpoint endpoint = esiRequestTestCase.EndpointBuilder.Build();
		request.Prepare(endpoint);
		request.RequestUrl.ShouldBe(esiRequestTestCase.ExpectedOutput);
	}

	[TestCaseSource(nameof(EndpointBodyTests))]
	public async Task EsiRequest_RequestBodyIsPrepared(EsiRequestTestCase esiRequestTestCase)
	{
		EsiRequest request = new(esiRequestTestCase.ParameterConfig);

		request.Prepare(esiRequestTestCase.EndpointBuilder.Build());

		request.Content.ShouldBeOfType<StringContent>();
		string body = await request.Content.ReadAsStringAsync();
		body.ShouldBe(esiRequestTestCase.ExpectedOutput);
	}


	[Test]
	public void EsiRequest_RequestMethodIsPrepared_FromEndpoint()
	{
		EsiEndpointBuilder endpoint = new("endpointId")
		{
			HttpMethodType = HttpMethodType.Post, Route = "/unit/{testId}/test"
		};
		EsiRequest request = new(p =>
		{
			p.Route["testId"] = "4444";
		});

		request.Prepare(endpoint.Build());
		request.Method.ShouldBe(HttpMethod.Post);
	}

	[Test]
	public void EsiRequest_PrepareMethodThrows_IfRequestHasNoTokenAndIsPreparedWithProtectedEndpoint()
	{
		EsiEndpointBuilder endpointBuilder = new("endpointId")
		{
			HttpMethodType = HttpMethodType.Post,
			Route = "/unit/{testId}/test",
			AuthenticatedEndpoint = true,
			Scope = "esi-characters.read_titles.v1"
		};

		EsiRequest request = new(p =>
		{
			p.Route["testId"] = "4444";
		});
		EsiEndpoint endpoint = endpointBuilder.Build();
		Should.Throw<InvalidOperationException>(() => request.Prepare(endpoint));
	}

	[Test]
	public void EsiRequest_SetsBearerToken_IfRequestHasTokenAndIsPreparedWithProtectedEndpoint()
	{
		EsiEndpointBuilder endpointBuilder = new("endpointId")
		{
			HttpMethodType = HttpMethodType.Post,
			Route = "/unit/{testId}/test",
			AuthenticatedEndpoint = true,
			Scope = "esi-characters.read_titles.v1"
		};
		string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
		EsiRequest request = new(p =>
		{
			p.Route["testId"] = "4444";
		}) { Token = token };
		EsiEndpoint endpoint = endpointBuilder.Build();
		request.Prepare(endpoint);
		request.Headers.Authorization.ShouldNotBeNull();
		request.Headers.Authorization.Parameter.ShouldBe(token);
	}

	public record EsiRequestTestCase(
		EsiEndpointBuilder EndpointBuilder,
		Action<RequestParameters> ParameterConfig,
		string ExpectedOutput);
}
