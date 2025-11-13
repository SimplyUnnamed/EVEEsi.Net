using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdMedalsGet" />
/// </summary>
public record CharacterMedalGraphic
{
	[JsonPropertyName("color")] public long? Color { get; init; }

	[JsonPropertyName("graphic")] public string Graphic { get; init; } = null!;

	[JsonPropertyName("layer")] public long Layer { get; init; }

	[JsonPropertyName("part")] public long Part { get; init; }
}
