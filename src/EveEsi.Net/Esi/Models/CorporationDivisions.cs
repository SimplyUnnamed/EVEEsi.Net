using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationDivision
{
	[JsonPropertyName("division")]
	public long DivisionId { get; init; }
	[JsonPropertyName("name")]
	public string DivisionName { get; init; } = null!;
}

public sealed record CorporationDivisions
{
	
	[JsonPropertyName("hanger")]
	public CorporationDivision[] Hanger { get; init; } = [];
	
	[JsonPropertyName("wallet")]
	public CorporationDivision[] Wallet { get; init; } = [];
}
