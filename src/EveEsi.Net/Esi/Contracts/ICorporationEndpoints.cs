using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface ICorporationEndpoints
{
	/// <summary>
	///     Get a list of npc corporations
	///     <br /><br />
	///     This route expires daily at 11:05
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsNpccorps" />
	/// </remarks>
	/// <returns>A Request client</returns>
	IRequestClient<long[]> GetNpcCorporations();

	/// <summary>
	///     Public information about a corporation
	///     <br /><br />
	///     This route is cached for an hour.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCorporationsCorporationId" />
	/// </remarks>
	/// <param name="corporationId">The ID of the corporation</param>
	/// <returns>A Request client</returns>
	IRequestClient<CorporationInformation> GetCorporationInformation(long corporationId);
}
