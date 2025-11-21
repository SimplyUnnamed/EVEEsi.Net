using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationStanding
{
	[JsonPropertyName("from_id")]
	public long FromId { get; init; }
	[JsonPropertyName("from_type")]
	public CorporationStandingFromType FromType { get; init; }
	[JsonPropertyName("standing")]
	public double Standing { get; init; }
}
