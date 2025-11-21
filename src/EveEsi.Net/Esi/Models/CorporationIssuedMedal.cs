using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationIssuedMedal
{
	/// <summary>
	/// ID of the character who was rewarded this medal
	/// </summary>
	[JsonPropertyName("character_id")]
	public long CharacterId { get; init; }
	
	
	[JsonPropertyName("issued_at")]
	public DateTime IssuedAt { get; init; }
	
	/// <summary>
	/// ID of the character who issued the medal
	/// </summary>
	[JsonPropertyName("issue_id")]
	public long IssuerId { get; init; }
	
	[JsonPropertyName("medal_id")]
	public long MedalId { get; init; }

	[JsonPropertyName("reason")]
	public string Reason { get; init; } = null!;
	
	[JsonPropertyName("status")]
	public MedalStatus Status { get; init; }
}
