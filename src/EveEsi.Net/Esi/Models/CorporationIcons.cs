using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record CorporationIcons
{
	[JsonPropertyName("px128x128")]
	public string X128 { get; init; } = null!;
	[JsonPropertyName("px256x256")]
	public string X256 { get; init; } = null!;
	[JsonPropertyName("px64x64")]
	public string X64 { get; init; } = null!;
	
};
