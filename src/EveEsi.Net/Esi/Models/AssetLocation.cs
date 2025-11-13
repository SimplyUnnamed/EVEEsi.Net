using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record AssetLocation
{
	[JsonPropertyName("item_id")] public required long ItemId { get; init; }

	[JsonPropertyName("position")] public required Position Position { get; init; }
}
