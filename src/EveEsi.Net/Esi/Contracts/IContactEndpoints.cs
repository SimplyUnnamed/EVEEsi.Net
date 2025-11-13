using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface IContactEndpoints
{
	/// <summary>
	///     Return contacts of an alliance
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetAlliancesAllianceIdContacts" />
	/// </remarks>
	/// <param name="allianceId">The ID of the alliance</param>
	/// <param name="token">Access Token for the character</param>
	/// <param name="page">Which page of results to fetch</param>
	/// <returns>A Request client to get an alliance's contacts</returns>
	IRequestClient<AllianceContact[]> GetAllianceContacts(long allianceId, string token, int? page = null);

	/// <summary>
	///     Return custom labels for an alliance's contacts
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetAlliancesAllianceIdContactsLabels" />
	/// </remarks>
	/// <param name="allianceId">The ID of the alliance</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get an alliance's contact labels</returns>
	IRequestClient<ContactLabel[]> GetAllianceContactLabels(long allianceId, string token);

	/// <summary>
	///     Bulk delete contacts
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/DeleteCharactersCharacterIdContacts" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="contactIds">
	///     List of contact Ids to remove
	///     <c>Requires Between 1 and 20 Contact Id's (inclusive)</c>
	/// </param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get an alliance's contact labels</returns>
	IRequestClient DeleteContacts(long characterId, IEnumerable<long> contactIds, string token);

	/// <summary>
	///     Return contacts of a character
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdContacts" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get an alliance's contact labels</returns>
	IRequestClient<CharacterContact[]> GetCharacterContacts(long characterId, string token, int? page = null);

	/// <summary>
	///     Bulk add contacts with same settings
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/PostCharactersCharacterIdContacts" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <param name="contactIds">
	///     Id's of characters to add as contacts
	///     <c>n = between 1 and 100</c>
	/// </param>
	/// <param name="standing">Standings to add the contacts at</param>
	/// <param name="labels">
	///     Id's of labels to assign
	///     <c>n = between 0 and 63</c>
	/// </param>
	/// <param name="watched">watch the characters or not</param>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if collections to not fit specification</exception>
	/// <returns>A Request client to get an alliance's contact labels</returns>
	IRequestClient<long[]> AddCharacterContact(long characterId, string token, IEnumerable<long> contactIds,
		double standing, IEnumerable<long>? labels = null, bool? watched = null);

	/// <summary>
	///     Bulk edit contacts with same settings
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/PutCharactersCharacterIdContacts" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <param name="contactIds">
	///     Id's of characters to add as contacts
	///     <c>n = between 1 and 100</c>
	/// </param>
	/// <param name="standing">Standings to add the contacts at</param>
	/// <param name="labels">
	///     Id's of labels to assign
	///     <c>n = between 0 and 63</c>
	/// </param>
	/// <param name="watched">watch the characters or not</param>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if collections to not fit specification</exception>
	/// <returns>A Request client to get an alliance's contact labels</returns>
	IRequestClient EditCharacterContacts(long characterId, string token, IEnumerable<long> contactIds, double standing,
		IEnumerable<long>? labels = null, bool? watched = null);

	/// <summary>
	///     Return custom labels for a character's contacts
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdContactsLabels" />
	/// </remarks>
	/// <param name="characterId">The ID of the character</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get a character's contact labels</returns>
	IRequestClient<ContactLabel[]> GetCharacterContactLabels(long characterId, string token);

	/// <summary>
	///     Return contacts of a corporation
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdContacts" />
	/// </remarks>
	/// <param name="corporationId">The ID of the Corporation</param>
	/// <param name="token">Access Token for the character</param>
	/// <param name="page">The page of contacts to get</param>
	/// <returns>A Request client to get a corporation's contacts</returns>
	IRequestClient<CorporationContact[]> GetCorporationContact(long corporationId, string token, int? page = null);

	/// <summary>
	///     Return custom labels for a corporation's contacts
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdContactsLabels" />
	/// </remarks>
	/// <param name="corporationId">The ID of the Corporation</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get a corporation's contact labels</returns>
	IRequestClient<ContactLabel[]> GetCorporationContactLabels(long corporationId, string token);
}
