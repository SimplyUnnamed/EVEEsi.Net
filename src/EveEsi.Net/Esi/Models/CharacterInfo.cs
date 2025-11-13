using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdGet" />
/// </summary>
public record CharacterInfo
{
	[JsonPropertyName("alliance_id")] public long? AllianceId { get; init; }

	[JsonPropertyName("birthday")] public required DateTime Birthday { get; init; }

	[JsonPropertyName("bloodline_id")] public required long BloodlineId { get; init; }

	[JsonPropertyName("corporation_id")] public required long CorporationId { get; init; }

	[JsonPropertyName("description")] public string? Description { get; init; } = "";

	[JsonPropertyName("faction_id")] public long? FactionId { get; init; }

	[JsonPropertyName("gender")] public required CharacterGender Gender { get; init; }

	[JsonPropertyName("name")] public required string Name { get; init; }

	[JsonPropertyName("race_id")] public required long RaceId { get; init; }

	[JsonPropertyName("security_status")] public double? SecurityStatus { get; init; }

	[JsonPropertyName("title")] public string? Title { get; init; }
}
