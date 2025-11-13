using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface ICloneEndpoints
{
	/// <summary>
	///     A list of the character's clones
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdClones" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get a character's clones</returns>
	IRequestClient<CharacterCloneDetails> GetCharacterClones(long characterId, string token);

	/// <summary>
	///     Return implants on the active clone of a character
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdImplants" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get a character's current implants</returns>
	IRequestClient<long[]> GetCharacterImplants(long characterId, string token);
}
