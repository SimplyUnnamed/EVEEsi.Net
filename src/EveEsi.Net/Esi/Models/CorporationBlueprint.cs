using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationBlueprint
{
	/// <summary>
	/// Unique ID for this item.
	/// </summary>
	[JsonPropertyName("item_id")]
	public long ItemId { get; init; }
	
	/// <summary>
	/// Type of the location_id
	/// </summary>
	[JsonPropertyName("location_flag")]
	public CorporationItemLocationFlag LocationFlag { get; init; }
	
	/// <summary>
	/// References a station, a ship or an item_id if this blueprint is located within a container.
	/// </summary>
	[JsonPropertyName("location_id")]
	public long LocationId { get; init; }
	
	/// <summary>
	/// Material Efficiency Level of the blueprint.
	/// </summary>
	[JsonPropertyName("material_efficiency")]
	public long MaterialEfficiency { get; init; }
	
	/// <summary>
	/// A range of numbers with a minimum of -2 and no maximum value where -1 is an original and -2 is a copy.
	/// It can be a positive integer if it is a stack of blueprint originals fresh from the market
	/// (e.g. no activities performed on them yet).
	/// </summary>
	[JsonPropertyName("quantity")]
	public long Quantity { get; init; }
	
	/// <summary>
	/// Number of runs remaining if the blueprint is a copy, -1 if it is an original.
	/// </summary>
	[JsonPropertyName("runs")]
	public long Runs { get; init; }
 
	/// <summary>
	/// Time Efficiency Level of the blueprint.
	/// </summary>
	[JsonPropertyName("time_efficiency")]
	public long TimeEfficiency { get; init; }
	
	[JsonPropertyName("type_id")]
	public long TypeId { get; init; }
}
