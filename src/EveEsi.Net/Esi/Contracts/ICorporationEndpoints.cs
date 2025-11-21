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

	///   <summary>
	///      Returns a corporation's medals
	///  	<br/>
	///      <b>Cache:</b> 1 Hour. <br />
	///  	<b>Rate Limit Group:</b> <c>corp-detail</c> <br/>
	/// 		<b>Scope:</b> <c>esi-corporations.read_medals.v1</c> <br/>
	///   </summary>
	///   <remarks>
	///       <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdMedals" />
	///   </remarks>
	///   <param name="corporationId">The ID of the corporation</param>
	///   <param name="token">The AccessToken of the character</param>
	///   <param name="page">Which page of results to return.</param>
	///   <returns>A Request client</returns>
	IRequestClient<CorporationMedal[]> GetCorporationMedals(long corporationId, string token, int? page = null);
	
	///   <summary>
	///     Returns medals issued by a corporation <br/>
	///     This route requires the token's character to have the following role: <c>Director.</c>
	///  	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  	<b>Rate Limit Group:</b> <c>corp-detail</c> <br/>
	/// 	<b>Scope:</b> <c>esi-corporations.read_medals.v1</c> <br/>
	///   </summary>
	///   <remarks>
	///       <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdMedalsIssued" />
	///   </remarks>
	///   <param name="corporationId">The ID of the corporation</param>
	///   <param name="token">The AccessToken of the character</param>
	///   <param name="page">Which page of results to return.</param>
	///   <returns>A Request client</returns>
	IRequestClient<CorporationIssuedMedal[]> GetCorporationIssuedMedals(long corporationId, string token, int? page = null);
	
	///   <summary>
	///     Return the current member list of a corporation, the token's character need to be a member of the corporation. <br/>
	///     This route requires the token's character to have the following role: <c>Director.</c>
	///  	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  	<b>Rate Limit Group:</b> <c>corp-member</c> <br/>
	/// 	<b>Scope:</b> <c>esi-corporations.read_corporation_membership.v1</c> <br/>
	///   </summary>
	///   <remarks>
	///       <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdMembers" />
	///   </remarks>
	///   <param name="corporationId">The ID of the corporation</param>
	///   <param name="token">The AccessToken of the character</param>
	///   <returns>A Request client</returns>
	IRequestClient<long[]> GetCorporationMembers(long corporationId, string token);

	///   <summary>
	///     Return a corporation's member limit, not including CEO himself<br/>
	///     This route requires the token's character to have the following role: <c>Director.</c>
	///  	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  	<b>Rate Limit Group:</b> <c>corp-member</c> <br/>
	/// 	<b>Scope:</b> <c>esi-corporations.track_members.v1</c> <br/>
	///   </summary>
	///   <remarks>
	///       <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdMembersLimit" />
	///   </remarks>
	///   <param name="corporationId">The ID of the corporation</param>
	///   <param name="token">The AccessToken of the character</param>
	///   <returns>A Request client</returns>
	IRequestClient<long> GetCorporationMemberLimit(long corporationId, string token);
	
	///   <summary>
	///     Returns a corporation's members' titles<br/>
	///     This route requires the token's character to have the following role: <c>Director.</c>
	///  	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  	<b>Rate Limit Group:</b> <c>corp-member</c> <br/>
	/// 	<b>Scope:</b> <c>esi-corporations.read_titles.v1.</c> <br/>
	///   </summary>
	///   <remarks>
	///       <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdMembersLimit" />
	///   </remarks>
	///   <param name="corporationId">The ID of the corporation</param>
	///   <param name="token">The AccessToken of the character</param>
	///   <returns>A Request client</returns>
	IRequestClient<CorporationMemberTitle[]> GetMemberTitles(long corporationId, string token);
	
	///   <summary>
	///     Returns additional information about a corporation's members which helps tracking their activities<br/>
	///     This route requires the token's character to have the following role: <c>Director.</c>
	///  	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  	<b>Rate Limit Group:</b> <c>corp-member</c> <br/>
	/// 	<b>Scope:</b> <c>esi-corporations.track_members.v1</c> <br/>
	///   </summary>
	///   <remarks>
	///       <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdMembertracking" />
	///   </remarks>
	///   <param name="corporationId">The ID of the corporation</param>
	///   <param name="token">The AccessToken of the character</param>
	///   <returns>A Request client</returns>
	IRequestClient<CorporationMemberTracking[]> GetMemberTracking(long corporationId, string token);
	
	///   <summary>
	///     Return the roles of all members if the character has the personnel manager role or any grantable role.<br/>
	///  	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  	<b>Rate Limit Group:</b> <c>corp-member</c> <br/>
	/// 	<b>Scope:</b> <c>esi-corporations.read_corporation_membership.v1</c> <br/>
	///   </summary>
	///   <remarks>
	///       <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdRoles" />
	///   </remarks>
	///   <param name="corporationId">The ID of the corporation</param>
	///   <param name="token">The AccessToken of the character</param>
	///   <returns>A Request client</returns>
	IRequestClient<CorporationMemberRole[]> GetCorporationMemberRoles(long corporationId, string token);

	///    <summary>
	///      Return how roles have changed for a coporation's members, up to a month<br/>
	/// 		This route requires the token's character to have the following role: <c>Director.</c> <br/>
	///   	<br/>
	///      <b>Cache:</b> 1 Hour. <br />
	///   	<b>Rate Limit Group:</b> <c>corp-member</c> <br/>
	///  	<b>Scope:</b> <c>esi-corporations.read_corporation_membership.v1</c> <br/>
	///    </summary>
	///    <remarks>
	///        <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdRolesHistory" />
	///    </remarks>
	///    <param name="corporationId">The ID of the corporation</param>
	///    <param name="token">The AccessToken of the character</param>
	///    <param name="page">Which page of results to return.</param>
	///    <returns>A Request client</returns>
	IRequestClient<CorporationMemberRoleHistory[]> GetCorporationMemberRoleHistory(long corporationId, string token, int? page = null);
	
	///    <summary>
	///			Return the current shareholders of a corporation.<br/>
	/// 		This route requires the token's character to have the following role: <c>Director.</c> <br/>
	///   	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///   	<b>Rate Limit Group:</b> <c>corp-detail</c> <br/>
	///  	<b>Scope:</b> <c>esi-wallet.read_corporation_wallets.v1</c> <br/>
	///    </summary>
	///    <remarks>
	///        <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdShareholders" />
	///    </remarks>
	///    <param name="corporationId">The ID of the corporation</param>
	///    <param name="token">The AccessToken of the character</param>
	///    <param name="page">Which page of results to return.</param>
	///    <returns>A Request client</returns>
	IRequestClient<CorporationShareholder[]> GetCorporationShareholders(long corporationId, string token, int? page = null);
	
	///    <summary>
	///			Return corporation standings from agents, NPC corporations, and factions<br/>
	///   	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///   	<b>Rate Limit Group:</b> <c>corp-member</c> <br/>
	///  	<b>Scope:</b> <c>esi-corporations.read_standings.v1</c> <br/>
	///    </summary>
	///    <remarks>
	///        <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdStandings" />
	///    </remarks>
	///    <param name="corporationId">The ID of the corporation</param>
	///    <param name="token">The AccessToken of the character</param>
	///    <param name="page">Which page of results to return.</param>
	///    <returns>A Request client</returns>
	IRequestClient<CorporationStanding[]> GetCorporationStandings(long corporationId, string token, int? page = null);
	
	///    <summary>
	///			Returns list of corporation starbases (POSes)<br/>
	///   	<br/>
	///     <b>Cache:</b> 1 Hour. <br />
	///  	<b>Scope:</b> <c>esi-corporations.read_starbases.v1</c> <br/>
	///    </summary>
	///    <remarks>
	///        <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdStarbases" />
	///    </remarks>
	///    <param name="corporationId">The ID of the corporation</param>
	///    <param name="token">The AccessToken of the character</param>
	///    <param name="page">Which page of results to return.</param>
	///    <returns>A Request client</returns>
	IRequestClient<CorporationStarbase[]> GetCorporationStarbases(long corporationId, string token, int? page = null);

	///     <summary>
	/// 		Returns various settings and fuels of a starbase (POS)<br/>
	///    		<br/>
	///			<b>Cache:</b> 1 Hour. <br />
	///   		<b>Scope:</b> <c>esi-corporations.read_starbases.v1</c> <br/>
	///     </summary>
	///     <remarks>
	///         <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdStarbasesStarbaseId" />
	///     </remarks>
	///     <param name="corporationId">The ID of the corporation</param>
	///     <param name="starbaseId">An EVE starbase (POS) ID</param>
	///     <param name="systemId">The solar system this starbase (POS) is located in</param>
	///     <param name="token">The AccessToken of the character</param>
	///     <returns>A Request client</returns>
	IRequestClient<CorporationStarbaseInfo> GetCorporationStarbaseInfo(long corporationId, long starbaseId, long systemId,
		string token);

	///      <summary>
	///  		Get a list of corporation structures. This route's version includes the changes to structures
	/// 			detailed in this blog: <see href="https://www.eveonline.com/article/upwell-2.0-structures-changes-coming-on-february-13th"/><br/>
	/// 			This route requires the token's character to have the following role: <c>Station_Manager</c>.<br/>
	///     	<br/>
	///			<b>Cache:</b> 1 Hour. <br />
	///    		<b>Scope:</b> <c>esi-corporations.read_structures.v1</c> <br/>
	///      </summary>
	///      <remarks>
	///          <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdStructures" />
	///      </remarks>
	///      <param name="corporationId">The ID of the corporation</param>
	///      <param name="token">The AccessToken of the character</param>
	///      <param name="page">Which page of results to return</param>
	///      <returns>A Request client</returns>
	IRequestClient<CorporationStructure[]> GetCorporationStructures(long corporationId, string token, int? page = null);
	
	///      <summary>
	///  		Returns a corporation's titles<br/>
	///			This route requires the token's character to have the following role: <c>Director</c>.<br/>
	///     	<br/>
	///			<b>Cache:</b> 1 Hour. <br />
	///			<b>Rate Limit Group:</b> <c>corp-detail</c> <br/>
	///    		<b>Scope:</b> <c>esi-corporations.read_titles.v1</c> <br/>
	///      </summary>
	///      <remarks>
	///          <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationIdStructures" />
	///      </remarks>
	///      <param name="corporationId">The ID of the corporation</param>
	///      <param name="token">The AccessToken of the character</param>
	///      <returns>A Request client</returns>
	IRequestClient<CorporationTitle[]> GetCorporationTitles(long corporationId, string token);
	
}
