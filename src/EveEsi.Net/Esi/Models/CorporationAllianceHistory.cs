using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;


public sealed record CorporationAllianceHistory
{
	[JsonPropertyName("alliance_id")]
	public long? AllianceId { get; init; }
	
	/// <summary>
	/// True if the alliance has been closed
	/// </summary>
	[JsonPropertyName("is_deleted")]
	public bool IsDeleted { get; init; }
	
	/// <summary>
	/// An incrementing ID that can be used to canonically establish order of records in cases where dates may be ambiguous
	/// </summary>
	[JsonPropertyName("record_id")]
	public long RecordId { get; init; }
	
	[JsonPropertyName("start_date")]
	public DateTime StartDate { get; init; }
};
