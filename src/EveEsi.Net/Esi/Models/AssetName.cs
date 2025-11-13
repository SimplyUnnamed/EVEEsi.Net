using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record AssetName
{
	[JsonPropertyName("item_id")] public required long ItemId { get; init; }

	[JsonPropertyName("name")] public required string Name { get; init; }
}
