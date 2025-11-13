using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdBlueprintsGet" />
/// </summary>
public record CharacterBlueprint
{
	[JsonPropertyName("item_id")] public required long ItemId { get; init; }

	[JsonPropertyName("location_flag")] public required CharacterItemLocationFlag LocationFlag { get; init; }

	[JsonPropertyName("location_id")] public required long LocationId { get; init; }

	[JsonPropertyName("material_efficiency")]
	public required long MaterialEfficiency { get; init; }

	[JsonPropertyName("quantity")] public required long Quantity { get; init; }

	[JsonPropertyName("runs")] public required long Runs { get; init; }

	[JsonPropertyName("time_efficiency")] public required long TimeEfficiency { get; init; }

	[JsonPropertyName("type_id")] public required long TypeId { get; init; }
}
