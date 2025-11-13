using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdStandingsGet" />
/// </summary>
public record CharacterStanding
{
	[JsonPropertyName("from_id")] public long FromId { get; init; }

	[JsonPropertyName("standing")] public double Standing { get; init; }

	[JsonPropertyName("from_type")] public StandingsFromType StandingType { get; init; }
}
