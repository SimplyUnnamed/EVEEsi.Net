namespace EveEsi.Net.Esi.Models;

public record CorporationStarbase
{
	public long? MoonId { get; init; }
	
	public DateTime? OnlineSince { get; init; }

	public DateTime? ReinforcedUntil { get; init; }
	
	public long StarbaseId { get; init; }
	
	public long SystemId { get; init; }
	
	public long TypeId { get; init; }
	
	public DateTime? UnanchorAt { get; init; }
}
