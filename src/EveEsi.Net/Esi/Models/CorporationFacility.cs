using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationFacility
{
	[JsonPropertyName("facility_id")]
	public long FacilityId { get; init; }
	[JsonPropertyName("system_id")]
	public long SystemId { get; init;}
	[JsonPropertyName("type_id")]
	public long TypeId { get; init; }};
