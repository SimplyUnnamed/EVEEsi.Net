using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationMemberTracking
{
	[JsonPropertyName("base_id")]
	public long? BaseId { get; init; }
	
	[JsonPropertyName("character_id")]
	public long CharacterId { get; init; }
	
	[JsonPropertyName("location_id")]
	public long? LocationId { get; init; }

	[JsonPropertyName("ship_type_id")]
	public long? ShipTypeId { get; init; }
	
	[JsonPropertyName("logoff_date")]
	public DateTime? LogoffDate { get; init; }
	
	[JsonPropertyName("logon_date")]
	public DateTime? LogonDate { get; init; }
}
