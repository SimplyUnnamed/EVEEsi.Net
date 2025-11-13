using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     Corporation assets
///     <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdAssetsGet">CharactersCharacterIdAssetsGet</see>
/// </summary>
public class CorporationAsset
{
	[JsonPropertyName("is_blueprint_copy")]
	public bool IsBlueprintCopy { get; init; }

	[JsonPropertyName("is_singleton")] public bool IsSingleton { get; init; }

	[JsonPropertyName("item_id")] public long ItemId { get; init; }

	[JsonPropertyName("location_flag")] public CorporationItemLocationFlag LocationFlag { get; init; }

	[JsonPropertyName("location_id")] public long LocationId { get; init; }

	[JsonPropertyName("location_type")] public ItemLocationType LocationType { get; init; }

	[JsonPropertyName("quantity")] public long Quantity { get; init; }

	[JsonPropertyName("type_id")] public long TypeId { get; init; }
}
