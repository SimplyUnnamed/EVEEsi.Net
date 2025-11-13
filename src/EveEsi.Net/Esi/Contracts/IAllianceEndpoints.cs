using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface IAllianceEndpoints
{
	/// <summary>
	///     Public information of an alliance
	/// </summary>
	/// <param name="allianceId">The alliance id</param>
	IRequestClient<AllianceInfo> GetAllianceInfo(long allianceId);

	/// <summary>
	///     Get all player alliances
	/// </summary>
	IRequestClient<long[]> ListAllAlliances();

	/// <summary>
	///     List all corporations of an alliance
	/// </summary>
	/// <param name="allianceId">The alliance id</param>
	IRequestClient<long[]> ListAllianceCorporations(long allianceId);

	/// <summary>
	///     Get the icon urls for the alliance logo's
	/// </summary>
	/// <param name="allianceId">The alliance id</param>
	IRequestClient<AllianceLogo> GetAllianceLogo(long allianceId);
}
