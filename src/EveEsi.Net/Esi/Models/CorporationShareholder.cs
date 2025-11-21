using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationShareholder
{
	[JsonPropertyName("share_count")]
	public long ShareCount { get; init; }
	[JsonPropertyName("shareholder_id")]
	public long ShareholderId { get; init; }
	[JsonPropertyName("shareholder_type")]
	public CorporationShareholderType ShareholderType { get; init; }
}
