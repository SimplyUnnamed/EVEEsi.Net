using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;
using Endpoints = EveEsi.Net.ESI.Endpoints.Clones;

namespace EveEsi.Net.Handlers;

internal class ClonesEndpoints(IEsiRequestClientFactory clientFactory) : ICloneEndpoints
{
	/// <inheritdoc />
	public IRequestClient<CharacterCloneDetails> GetCharacterClones(long characterId, string token)
	{
		return clientFactory.CreateClient<CharacterCloneDetails>(Endpoints.CloneList, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<long[]> GetCharacterImplants(long characterId, string token)
	{
		return clientFactory.CreateClient<long[]>(Endpoints.CloneImplants, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}
}
