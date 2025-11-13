using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdMedalsGet" />
/// </summary>
public record CharacterMedal
{
	[JsonPropertyName("corporation_id")] public long CorporationId { get; init; }

	[JsonPropertyName("date")] public DateTime Date { get; init; }

	[JsonPropertyName("description")] public string Description { get; init; } = null!;

	[JsonPropertyName("issuer_id")] public long IssuerId { get; init; }

	[JsonPropertyName("medal_id")] public long MedalId { get; init; }

	[JsonPropertyName("reason")] public string Reason { get; init; } = null!;

	[JsonPropertyName("title")] public string Title { get; init; } = null!;

	[JsonPropertyName("status")] public MedalStatus Status { get; init; }

	[JsonPropertyName("graphics")] public CharacterMedalGraphic Graphic { get; init; } = null!;
}
