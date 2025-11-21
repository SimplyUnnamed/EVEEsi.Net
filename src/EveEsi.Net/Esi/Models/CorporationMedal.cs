using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationMedal
{
	[JsonPropertyName("created_at")]
	public DateTime CreatedAt { get; init; }
	
	/// <summary>
	/// ID of the character who created this medal
	/// </summary>
	[JsonPropertyName("creator_id")]
	public long CreatorId { get; init; }

	[JsonPropertyName("description")]
	public string Description { get; init; } = null!;
	
	[JsonPropertyName("medal_id")]
	public long MedalId { get; init; }
	
	[JsonPropertyName("title")]
	public string Title { get; init; } = null!;
}
