using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface ICorporationEndpoints
{
	/// <summary>
	///     Get a list of npc corporations
	///     <br />
	///     <b>Cache:</b> This route expires daily at 11:05
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsNpccorps" />
	/// </remarks>
	/// <returns>A Request client</returns>
	IRequestClient<long[]> GetNpcCorporations();

	/// <summary>
	///     Public information about a corporation
	///     <br />
	///     <b>Cache:</b> 1 Hour.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationId" />
	/// </remarks>
	/// <param name="corporationId">The ID of the corporation</param>
	/// <returns>A Request client</returns>
	IRequestClient<CorporationInformation> GetCorporationInformation(long corporationId);
	
	/// <summary>
	///     Get a list of all the alliances a corporation has been a member of
	///     <br />
	///     <b>Cache:</b> 1 Hour.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdAlliancehistory" />
	/// </remarks>
	/// <param name="corporationId">The ID of the corporation</param>
	/// <returns>A Request client</returns>
	IRequestClient<CorporationAllianceHistory[]> GetCorporationAllianceHistory(long corporationId);

	///  <summary>
	///     Returns a list of blueprints the corporation owns
	///     <br />
	/// 	This route requires the token's character to have the following role: <c>Director</c>.
	/// 	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///		<b>Scope:</b> <c>esi-corporations.read_blueprints.v1</c> <br/>
	///		<b>Rate Limit Group:</b> <c>corp-industry</c>
	///  </summary>
	///  <remarks>
	///      <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdAlliancehistory" />
	///  </remarks>
	///  <param name="corporationId">The ID of the corporation</param>
	///  <param name="token">The AccessToken of the character</param>
	///  <param name="page">Which page of results to return.</param>
	///  <returns>A Request client</returns>
	IRequestClient<CorporationBlueprint[]> GetCorporationBlueprints(long corporationId, string token, int? page = null);
	
	///  <summary>
	///     Returns logs recorded in the past seven days from all audit log secure containers (ALSC) owned by a given corporation
	///     <br />
	/// 	This route requires the token's character to have the following role: <c>Director</c>.
	/// 	<br/>
	///     <b>Cache:</b> 10 Minutes. <br />
	///		<b>Scope:</b> <c>esi-corporations.read_container_logs.v1</c> <br/>
	///  </summary>
	///  <remarks>
	///      <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdContainersLogs" />
	///  </remarks>
	///  <param name="corporationId">The ID of the corporation</param>
	///  <param name="token">The AccessToken of the character</param>
	///  <param name="page">Which page of results to return.</param>
	///  <returns>A Request client</returns>
	IRequestClient<CorporationAlscLog[]> GetCorporationAlscLogs(long corporationId, string token, int? page = null);
	
	///  <summary>
	///     Return corporation hangar and wallet division names, only show if a division is not using the default name
	///     <br />
	/// 	This route requires the token's character to have the following role: <c>Director</c>.
	/// 	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///		<b>Scope:</b> <c>esi-corporations.read_divisions.v1</c> <br/>
	///		<b>Rate Limit Group:</b> <c>corp-wallet</c>
	///  </summary>
	///  <remarks>
	///      <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdDivisions" />
	///  </remarks>
	///  <param name="corporationId">The ID of the corporation</param>
	///  <param name="token">The AccessToken of the character</param>
	///  <returns>A Request client</returns>
	IRequestClient<CorporationDivisions> GetCorporationDivisions(long corporationId, string token);
	
	///  <summary>
	///     Return a corporation's facilities
	///     <br />
	/// 	This route requires the token's character to have the following role: <c>Factory_Manager</c>.
	/// 	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///		<b>Scope:</b> <c>esi-corporations.read_facilities.v1</c> <br/>
	///  </summary>
	///  <remarks>
	///      <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdFacilities" />
	///  </remarks>
	///  <param name="corporationId">The ID of the corporation</param>
	///  <param name="token">The AccessToken of the character</param>
	///  <returns>A Request client</returns>
	IRequestClient<CorporationFacility[]> GetCorporationFacilities(long corporationId, string token);
	
	///  <summary>
	///     Get the icon urls for a corporation
	/// 	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  </summary>
	///  <remarks>
	///      <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdIcons" />
	///  </remarks>
	///  <param name="corporationId">The ID of the corporation</param>
	///  <returns>A Request client</returns>
	IRequestClient<CorporationIcons> GetCorporationIcons(long corporationId);
}
