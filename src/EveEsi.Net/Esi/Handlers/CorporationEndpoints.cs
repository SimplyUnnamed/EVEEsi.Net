using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;
using Endpoints = EveEsi.Net.ESI.Endpoints.Corporation;

namespace EveEsi.Net.Handlers;

internal class CorporationEndpoints(IEsiRequestClientFactory clientFactory) : ICorporationEndpoints
{
	/// <inheritdoc />
	public IRequestClient<long[]> GetNpcCorporations()
	{
		return clientFactory.CreateClient<long[]>(Endpoints.NpcCorporations, b => { });
	}

	/// <inheritdoc />
	public IRequestClient<CorporationInformation> GetCorporationInformation(long corporationId)
	{
		return clientFactory.CreateClient<CorporationInformation>(Endpoints.Information, b =>
		{
			b.Query[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		});
	}
}
