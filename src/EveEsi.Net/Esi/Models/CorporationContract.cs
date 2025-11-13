using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public record CorporationContract
{
	/// <summary>
	///     Who will accept the contract
	/// </summary>
	[JsonPropertyName("acceptor_id")]
	public long AcceptorId { get; init; }

	/// <summary>
	///     ID to whom the contract is assigned, can be alliance, corporation or character ID
	/// </summary>
	[JsonPropertyName("assignee_id")]
	public long AssigneeId { get; init; }

	/// <summary>
	///     To whom the contract is available
	/// </summary>
	[JsonPropertyName("availability")]
	public ContractAvailability Availability { get; init; }

	/// <summary>
	///     Buyout price (for Auctions only)
	/// </summary>
	[JsonPropertyName("buyout")]
	public double? Buyout { get; init; }

	/// <summary>
	///     Collateral price (for Couriers only)
	/// </summary>
	[JsonPropertyName("collateral")]
	public double? Collateral { get; init; }

	[JsonPropertyName("contact_id")] public long? ContractId { get; init; }

	/// <summary>
	///     Date of confirmation of contract
	/// </summary>
	[JsonPropertyName("date_accepted")]
	public DateTime? DateAccepted { get; init; }

	/// <summary>
	///     Date of completed of contract
	/// </summary>
	[JsonPropertyName("date_completed")]
	public DateTime? DateCompleted { get; init; }

	/// <summary>
	///     Expiration date of the contract
	/// </summary>
	[JsonPropertyName("date_expired")]
	public DateTime DateExpired { get; init; }

	/// <summary>
	///     Сreation date of the contract
	/// </summary>
	[JsonPropertyName("date_issued")]
	public DateTime DateIssued { get; init; }

	/// <summary>
	///     Number of days to perform the contract
	/// </summary>
	[JsonPropertyName("days_to_complete")]
	public long? DayToComplete { get; init; }

	/// <summary>
	///     End location ID (for Couriers contract)
	/// </summary>
	[JsonPropertyName("end_location_id")]
	public long? EndLocationId { get; init; }

	/// <summary>
	///     true if the contract was issued on behalf of the issuer's corporation
	/// </summary>
	[JsonPropertyName("for_corporation")]
	public bool ForCorporation { get; init; }

	/// <summary>
	///     Character's corporation ID for the issuer
	/// </summary>
	[JsonPropertyName("issuer_corporation_id")]
	public long IssuerCorporationId { get; init; }

	/// <summary>
	///     Character ID for the issuer
	/// </summary>
	[JsonPropertyName("issuer_id")]
	public long IssuerId { get; init; }

	/// <summary>
	///     Price of contract (for ItemsExchange and Auctions)
	/// </summary>
	[JsonPropertyName("price")]
	public double Price { get; init; }

	/// <summary>
	///     Remuneration for contract (for Couriers only)
	/// </summary>
	[JsonPropertyName("reward")]
	public double Reward { get; init; }

	/// <summary>
	///     Start location ID (for Couriers contract)
	/// </summary>
	[JsonPropertyName("start_location_id")]
	public long StartLocationId { get; init; }

	/// <summary>
	///     Status of the contract
	/// </summary>
	[JsonPropertyName("status")]
	public ContractStatus Status { get; init; }

	/// <summary>
	///     Title of the contract
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	///     Type of the contract
	/// </summary>
	[JsonPropertyName("type")]
	public ContractType ContractType { get; init; }

	/// <summary>
	///     Volume of items in the contract
	/// </summary>
	[JsonPropertyName("volume")]
	public double Volume { get; init; }

	[MemberNotNullWhen(true, nameof(Buyout))]
	[MemberNotNullWhen(true, nameof(Price))]
	public bool IsAuction => ContractType == ContractType.Auction;

	[MemberNotNullWhen(true, nameof(EndLocationId))]
	[MemberNotNullWhen(true, nameof(Reward))]
	[MemberNotNullWhen(true, nameof(StartLocationId))]
	public bool IsCourier => ContractType == ContractType.Courier;

	[MemberNotNullWhen(true, nameof(Price))]
	public bool IsItemExchange => ContractType == ContractType.ItemExchange;
}
