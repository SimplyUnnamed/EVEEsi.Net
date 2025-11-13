using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdPortraitGet" />
/// </summary>
public record CharacterPortrait
{
	[JsonPropertyName("px128x128")] public string P128 { get; init; } = null!;

	[JsonPropertyName("px256x256")] public string P256 { get; init; } = null!;

	[JsonPropertyName("px512x512")] public string P512 { get; init; } = null!;

	[JsonPropertyName("px64x64")] public string P64 { get; init; } = null!;
}
