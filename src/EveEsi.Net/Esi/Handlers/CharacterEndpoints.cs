using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Factories;
using Endpoints = EveEsi.Net.ESI.Endpoints.Characters;

namespace EveEsi.Net.Handlers;

internal class CharacterEndpoints(IEsiRequestClientFactory clientFactory) : ICharacterEndpoints
{
	/// <inheritdoc />
	public IRequestClient<CharacterAffiliation[]> GetCharacterAffiliation(long[] characterIds)
	{
		if (!characterIds.ListWithinSize(10000))
		{
			throw new ArgumentOutOfRangeException(nameof(characterIds),
				"There must be between 1 and 1000 characters ids");
		}

		return clientFactory.CreateClient<CharacterAffiliation[]>(Endpoints.Affilation, p =>
		{
			p.Body = characterIds;
		});
	}

	/// <inheritdoc />
	public IRequestClient<CharacterInfo> GetCharacterInfo(long characterId)
	{
		return clientFactory.CreateClient<CharacterInfo>(Endpoints.PublicInformation, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		});
	}

	/// <inheritdoc />
	public IRequestClient<CharacterAgentResearch[]> GetCharacterAgentResearch(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterAgentResearch[]>(Endpoints.AgentsResearch, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterBlueprint[]> GetCharacterBlueprints(long characterId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CharacterBlueprint[]>(Endpoints.Blueprints, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Query[ESI.Parameters.Query.Page] = page?.ToString() ?? "1";
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterCorporationHistory[]> GetCharacterCorporationHistory(long characterId)
	{
		return clientFactory.CreateClient<CharacterCorporationHistory[]>(Endpoints.CorporationHistory,
			p =>
			{
				p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			});
	}

	/// <inheritdoc />
	public IRequestClient<double> CalculateCSPACharge(long characterId, long[] otherCharacterIds, string token)
	{
		return clientFactory.CreateClient<double>(Endpoints.CSPA, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Body = otherCharacterIds;
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterJumpFatigue> GetCharacterJumpFatigue(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterJumpFatigue>(Endpoints.Fatigue, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterMedal[]> GetCharacterMedals(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterMedal[]>(Endpoints.Medals, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterNotification[]> GetCharacterNotifications(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterNotification[]>(Endpoints.Notifications, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterContactNotification[]> GetCharacterNotification(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterContactNotification[]>(Endpoints.ContactNotifications, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterPortrait> GetCharacterPortrait(long characterId)
	{
		return clientFactory.CreateClient<CharacterPortrait>(Endpoints.Portrait, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		});
	}

	/// <inheritdoc />
	public IRequestClient<CharacterCorporationRoles> GetCharacterCorporationRoles(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterCorporationRoles>(Endpoints.Roles, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterStanding[]> GetCharacterStandings(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterStanding[]>(Endpoints.Standings, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CharacterTitle[]> GetCharacterTitles(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterTitle[]>(Endpoints.Titles, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}
}
