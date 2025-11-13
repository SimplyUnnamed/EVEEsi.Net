using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface ICharacterEndpoints
{
	/// <summary>
	///     Bulk lookup of character IDs to corporation, alliance and faction
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/PostCharactersAffiliation" />
	/// </remarks>
	/// <param name="characterIds">
	///     List of Character IDs <c>Requires Between 1 and 1000 Character Id's (inclusive)</c>
	/// </param>
	/// <returns>Request Client  for A list of current Character Affiliations</returns>
	IRequestClient<CharacterAffiliation[]> GetCharacterAffiliation(long[] characterIds);

	/// <summary>
	///     Public information about a character
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterId" />
	/// </remarks>
	/// <param name="characterId">
	///     The ID of the character
	/// </param>
	/// <returns>Request Client for Character Public Information</returns>
	IRequestClient<CharacterInfo> GetCharacterInfo(long characterId);


	/// <summary>
	///     Return a list of agents research information for a character. The formula for finding the current research points
	///     with an agent is: <c>currentPoints = remainderPoints + pointsPerDay * days(currentTime - researchStartDate)</c>
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdAgentsResearch" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">The Access Token for the character</param>
	/// <returns>Request client to get Character Agent Research</returns>
	IRequestClient<CharacterAgentResearch[]> GetCharacterAgentResearch(long characterId, string token);

	/// <summary>
	///     Return a list of blueprints the character owns
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdBlueprints" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">The Access Token for the character</param>
	/// <param name="page">Which page of results to return.</param>
	/// <returns>Request client to get Character blueprints</returns>
	IRequestClient<CharacterBlueprint[]> GetCharacterBlueprints(long characterId, string token, int? page = null);


	/// <summary>
	///     Get a list of all the corporations a character has been a member of
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdBlueprints" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <returns>Request client to get Character corporation history</returns>
	IRequestClient<CharacterCorporationHistory[]> GetCharacterCorporationHistory(long characterId);

	/// <summary>
	///     Takes a source character ID in the url and a set of target character ID's in the body, returns a CSPA charge cost
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/PostCharactersCharacterIdCspa" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="otherCharacterIds">List of Character Ids</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>Request client to get Character corporation history</returns>
	IRequestClient<double> CalculateCSPACharge(long characterId, long[] otherCharacterIds, string token);

	/// <summary>
	///     Takes a source character ID in the url and a set of target character ID's in the body, returns a CSPA charge cost
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/PostCharactersCharacterIdCspa" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="otherCharacterIds">List of Character Ids</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>Request client to get Character corporation history</returns>
	IRequestClient<CharacterJumpFatigue> GetCharacterJumpFatigue(long characterId, string token);

	/// <summary>
	///     Return a list of medals the character has
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdMedals" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A request client for a list of medals</returns>
	IRequestClient<CharacterMedal[]> GetCharacterMedals(long characterId, string token);

	/// <summary>
	///     Return character notifications
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdNotifications" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A request client for a list of notifications</returns>
	IRequestClient<CharacterNotification[]> GetCharacterNotifications(long characterId, string token);

	/// <summary>
	///     Return notifications about having been added to someone's contact list
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdNotificationsContacts" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A request client for a list of contact notifications</returns>
	IRequestClient<CharacterContactNotification[]> GetCharacterNotification(long characterId, string token);

	/// <summary>
	///     Get portrait urls for a character
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdPortrait" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <returns>A request client for character portrait urls</returns>
	IRequestClient<CharacterPortrait> GetCharacterPortrait(long characterId);

	/// <summary>
	///     Returns a character's corporation roles
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdRoles" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A request client for a character's corporation roles</returns>
	IRequestClient<CharacterCorporationRoles> GetCharacterCorporationRoles(long characterId, string token);

	/// <summary>
	///     Return character standings from agents, NPC corporations, and factions
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdStandings" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A request client for a character's corporation roles</returns>
	IRequestClient<CharacterStanding[]> GetCharacterStandings(long characterId, string token);

	/// <summary>
	///     Returns a character's titles
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdTitles" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A request client for a character's corporation titles</returns>
	IRequestClient<CharacterTitle[]> GetCharacterTitles(long characterId, string token);
}
