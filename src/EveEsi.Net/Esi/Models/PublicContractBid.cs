using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record PublicContractBid
{
	/// <summary>
	///     The amount bid, in ISK
	/// </summary>
	[JsonPropertyName("amount")]
	public double Amount { get; init; }

	/// <summary>
	///     Unique ID for the bid
	/// </summary>
	[JsonPropertyName("bid_id")]
	public long BidId { get; init; }


	/// <summary>
	///     Datetime when the bid was placed
	/// </summary>
	[JsonPropertyName("date_bid")]
	public DateTime DateBid { get; init; }
}
