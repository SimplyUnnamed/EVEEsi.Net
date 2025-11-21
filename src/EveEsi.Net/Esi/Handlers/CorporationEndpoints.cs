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

	/// <inheritdoc />
	public IRequestClient<CorporationMedal[]> GetCorporationMedals(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationMedal[]>(Endpoints.Medals, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationIssuedMedal[]> GetCorporationIssuedMedals(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationIssuedMedal[]>(Endpoints.IssuedMedals, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<long[]> GetCorporationMembers(long corporationId, string token)
	{
		return clientFactory.CreateClient<long[]>(Endpoints.Members, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<long> GetCorporationMemberLimit(long corporationId, string token)
	{
		return clientFactory.CreateClient<long>(Endpoints.MembersLimit, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationMemberTitle[]> GetMemberTitles(long corporationId, string token)
	{
		return clientFactory.CreateClient<CorporationMemberTitle[]>(Endpoints.MembersTitles, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationMemberTracking[]> GetMemberTracking(long corporationId, string token)
	{
		return clientFactory.CreateClient<CorporationMemberTracking[]>(Endpoints.MemberTracking, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationMemberRole[]> GetCorporationMemberRoles(long corporationId, string token)
	{
		return clientFactory.CreateClient<CorporationMemberRole[]>(Endpoints.Roles, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationMemberRoleHistory[]> GetCorporationMemberRoleHistory(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationMemberRoleHistory[]>(Endpoints.RolesHistory, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationShareholder[]> GetCorporationShareholders(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationShareholder[]>(Endpoints.Shareholders, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationStanding[]> GetCorporationStandings(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationStanding[]>(Endpoints.Standings, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationStarbase[]> GetCorporationStarbases(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationStarbase[]>(Endpoints.Starbases, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationStarbaseInfo> GetCorporationStarbaseInfo(long corporationId, long starbaseId, long systemId, string token)
	{
		return clientFactory.CreateClient<CorporationStarbaseInfo>(Endpoints.StarbaseInfo, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.Route[ESI.Parameters.Route.StarbaseId] = starbaseId.ToString();
			b.Query[ESI.Parameters.Query.SystemId] = systemId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationStructure[]> GetCorporationStructures(long corporationId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CorporationStructure[]>(Endpoints.Structures, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationTitle[]> GetCorporationTitles(long corporationId, string token)
	{
		return clientFactory.CreateClient<CorporationTitle[]>(Endpoints.Titles, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}
}
