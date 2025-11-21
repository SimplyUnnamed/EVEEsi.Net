using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Factories;
using Endpoints = EveEsi.Net.ESI.Endpoints.Corporation;

namespace EveEsi.Net.Handlers;

internal sealed class CorporationEndpoints(IEsiRequestClientFactory clientFactory) : ICorporationEndpoints
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
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		});
	}

	/// <inheritdoc />
	public IRequestClient<CorporationAllianceHistory[]> GetCorporationAllianceHistory(long corporationId)
	{
		return clientFactory.CreateClient<CorporationAllianceHistory[]>(Endpoints.AllianceHistory, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		});
	}

	/// <inheritdoc />
	public IRequestClient<CorporationBlueprint[]> GetCorporationBlueprints(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationBlueprint[]>(Endpoints.Blueprints, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationAlscLog[]> GetCorporationAlscLogs(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationAlscLog[]>(Endpoints.ContainersLogs, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationDivisions> GetCorporationDivisions(long corporationId, string token)
	{
		return clientFactory.CreateClient<CorporationDivisions>(Endpoints.Divisions, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		},token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationFacility[]> GetCorporationFacilities(long corporationId, string token)
	{
		return clientFactory.CreateClient<CorporationFacility[]>(Endpoints.Facilities, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationIcons> GetCorporationIcons(long corporationId)
	{
		return clientFactory.CreateClient<CorporationIcons>(Endpoints.Icons, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		});
	}
}
