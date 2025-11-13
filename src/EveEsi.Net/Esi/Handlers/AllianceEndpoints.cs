using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;

namespace EveEsi.Net.Handlers;

internal class AllianceEndpoints(IEsiRequestClientFactory clientFactory) : IAllianceEndpoints
{
	/// <inheritdoc />
	public IRequestClient<AllianceInfo> GetAllianceInfo(long allianceId)
	{
		return clientFactory.CreateClient<AllianceInfo>(ESI.Endpoints.Alliances.PublicInformation, parameters =>
		{
			parameters.Route[ESI.Parameters.Route.AllianceId] = allianceId.ToString();
		});
	}

	/// <inheritdoc />
	public IRequestClient<long[]> ListAllAlliances()
	{
		return clientFactory.CreateClient<long[]>(ESI.Endpoints.Alliances.ActiveAlliances, p =>
		{
		});
	}

	/// <inheritdoc />
	public IRequestClient<long[]> ListAllianceCorporations(long allianceId)
	{
		return clientFactory.CreateClient<long[]>(ESI.Endpoints.Alliances.CorporationsInAlliance, parameters =>
		{
			parameters.Route[ESI.Parameters.Route.AllianceId] = allianceId.ToString();
		});
	}

	/// <inheritdoc />
	public IRequestClient<AllianceLogo> GetAllianceLogo(long allianceId)
	{
		return clientFactory.CreateClient<AllianceLogo>(ESI.Endpoints.Alliances.AllianceIcon, parameters =>
		{
			parameters.Route[ESI.Parameters.Route.AllianceId] = allianceId.ToString();
		});
	}
}
