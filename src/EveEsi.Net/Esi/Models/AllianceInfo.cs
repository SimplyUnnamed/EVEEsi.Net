using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See
///     <see href="https://developers.eveonline.com/api-explorer#/schemas/AlliancesAllianceIdGet">AlliancesAllianceIdGet</see>
/// </summary>
public record AllianceInfo
{
	[JsonPropertyName("creator_corporation_id")]
	public long CreatorCorporationId { get; init; }

	[JsonPropertyName("creator_id")] public long CreatorId { get; init; }

	[JsonPropertyName("date_founded")] public DateTime DateFounded { get; init; }

	[JsonPropertyName("executor_corporation_id")]
	public long? ExecutorCorporationId { get; init; }

	[JsonPropertyName("faction_id")] public long? FactionId { get; init; }

	[JsonPropertyName("name")] public string Name { get; init; } = null!;

	[JsonPropertyName("ticker")] public string Ticker { get; init; } = null!;
}
