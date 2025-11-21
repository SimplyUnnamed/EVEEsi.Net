using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record CorporationMemberTitle
{
	[JsonPropertyName("character_id")]
	public long CharacterId { get; init; }

	/// <summary>
	/// A list of title_id
	/// </summary>
	[JsonPropertyName("titles")]
	public long[] Titles { get; init; } = [];
}
