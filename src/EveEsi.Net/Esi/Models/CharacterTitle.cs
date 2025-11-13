using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdTitlesGet" />
/// </summary>
public record CharacterTitle
{
	[JsonPropertyName("name")] public string Name { get; init; } = null!;

	[JsonPropertyName("title_id")] public long TitleId { get; init; }
}
