using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record CorporationInformation
{
	/// <summary>
	///     ID of the alliance that corporation is a member of, if any
	/// </summary>
	[JsonPropertyName("alliance_id")]
	public long? AllianceId { get; init; }

	[JsonPropertyName("ceo_id")] public long CeoId { get; init; }

	[JsonPropertyName("creator_id")] public long CreatorId { get; init; }

	[JsonPropertyName("date_founded")] public DateTime? DateFounded { get; init; }

	[JsonPropertyName("description")] public string? Description { get; init; }

	[JsonPropertyName("faction_id")] public long? FactionId { get; init; }

	[JsonPropertyName("home_station_id")] public long? HomeStationId { get; init; }

	[JsonPropertyName("member_count")] public long MemberCount { get; init; }

	/// <summary>
	///     the full name of the corporation
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = null!;

	[JsonPropertyName("shared")] public long? Shares { get; init; }

	[JsonPropertyName("tax_rate")] public double TaxRate { get; init; }

	[JsonPropertyName("ticker")] public string Ticker { get; init; } = null!;

	[JsonPropertyName("url")] public string? Url { get; init; }

	[JsonPropertyName("war_eligible")] public bool WarEligible { get; init; }
}
