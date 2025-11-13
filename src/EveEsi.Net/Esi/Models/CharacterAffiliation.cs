using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersAffiliationPost" />
/// </summary>
public record CharacterAffiliation
{
	[JsonPropertyName("alliance_id")] public long? AllianceId { get; init; }

	[JsonPropertyName("character_id")] public required long CharacterId { get; init; }

	[JsonPropertyName("corporation_id")] public required long CorporationId { get; init; }

	[JsonPropertyName("faction_id")] public long? FactionId { get; init; }
}
