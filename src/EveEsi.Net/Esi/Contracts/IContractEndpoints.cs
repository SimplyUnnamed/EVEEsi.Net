using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface IContractEndpoints
{
	/// <summary>
	///     Returns contracts available to a character, only if the character is issuer, acceptor or assignee. Only returns
	///     contracts no older than 30 days, or if the status is "in_progress".
	///     <br /><br />
	///     This route is cached for 5 minutes. <br /><br />
	///     This route requires the following scope: <c>esi-contracts.read_character_contracts.v1</c>.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdContracts" />
	/// </remarks>
	/// <param name="characterId">The ID of the Character</param>
	/// <param name="token">Access Token for the character</param>
	/// <param name="page">Which page of results to return. (Default 1)</param>
	/// <returns>A Request client to get a character's contracts</returns>
	IRequestClient<CharacterContract[]> GetCharacterContracts(long characterId, string token, int? page = null);

	/// <summary>
	///     Lists bids on a particular auction contract
	///     <br /><br />
	///     This route is cached for 5 minutes.
	///     <br /><br />
	///     This route requires the following scope: <c>esi-contracts.read_character_contracts.v1</c>.
	/// </summary>
	/// <remarks>
	///     <see
	///         href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdContractsContractIdBids" />
	/// </remarks>
	/// <param name="characterId">The ID of the Character</param>
	/// <param name="contractId">ID of a contract</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get a character's contracts bids</returns>
	IRequestClient<ContractBid[]> GetContractBids(long characterId, long contractId, string token);

	/// <summary>
	///     Lists items of a public contract
	///     <br /><br />
	///     This route is cached for an hour.
	///     <br /><br />
	///     This route requires the following scope: <c>esi-contracts.read_character_contracts.v1</c>.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetContractsPublicItemsContractId" />
	/// </remarks>
	/// <param name="characterId">The ID of the Character</param>
	/// <param name="contractId">ID of a contract</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get a character's contracts bids</returns>
	IRequestClient<ContractItem[]> GetContractItems(long characterId, long contractId, string token);

	/// <summary>
	///     Lists items of a public contract
	///     <br /><br />
	///     This route is cached for an hour.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetContractsPublicRegionId" />
	/// </remarks>
	/// <param name="regionId">An EVE region id</param>
	/// <param name="page">Which page of results to return.</param>
	/// <returns>A Request client to get a character's contracts bids</returns>
	IRequestClient<PublicContract[]> GetPublicContracts(long regionId, int? page = null);

	/// <summary>
	///     Lists bids on a public auction contract
	///     <br /><br />
	///     This route is cached for 5 minutes.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetContractsPublicBidsContractId" />
	/// </remarks>
	/// <param name="contractId">ID of a contract</param>
	/// <param name="page">Which page of results to return.</param>
	/// <returns>A Request client to get a character's contracts bids</returns>
	IRequestClient<PublicContractBid[]> GetPublicContractBids(long contractId, int? page = null);

	/// <summary>
	///     Lists items of a public contract
	///     <br /><br />
	///     This route is cached for an hour.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetContractsPublicItemsContractId" />
	/// </remarks>
	/// <param name="contractId">ID of a contract</param>
	/// <param name="page">Which page of results to return.</param>
	/// <returns>A Request client to get a character's contracts bids</returns>
	IRequestClient<PublicContractItem[]> GetPublicContractItems(long contractId, int? page = null);

	/// <summary>
	///     Returns contracts available to a corporation, only if the corporation is issuer, acceptor or assignee. Only returns
	///     contracts no older than 30 days, or if the status is "in_progress".
	///     <br /><br />
	///     This route is cached for 5 minutes.
	///     <br /><br />
	///     This route is part of the rate limit group <c>corp-contract</c>. This group is limited to 600 tokens per 15
	///     minutes.
	///     <br /><br />
	///     This route requires the following scope: <c>esi-contracts.read_corporation_contracts.v1</c>.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdContracts" />
	/// </remarks>
	/// <param name="corporationId">The ID of the corporation</param>
	/// <param name="token">Access Token for the character</param>
	/// <param name="page">Which page of results to return. (Default 1)</param>
	/// <returns>A Request client to get a character's contracts</returns>
	IRequestClient<CorporationContract[]> GetCorporationContracts(long corporationId, string token, int? page = null);

	/// <summary>
	///     Lists bids on a particular auction contract
	///     <br /><br />
	///     This route is cached for an hour.
	///     <br /><br />
	///     This route is part of the rate limit group <c>corp-contract</c>. This group is limited to 600 tokens per 15
	///     minutes.
	///     <br /><br />
	///     This route requires the following scope: <c>esi-contracts.read_corporation_contracts.v1</c>.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdContracts" />
	/// </remarks>
	/// <param name="corporationId">The ID of the corporation</param>
	/// <param name="contractId">ID of a contract</param>
	/// <param name="token">Access Token for the character</param>
	/// <param name="page">Which page of results to return. (Default 1)</param>
	/// <returns>A Request client to get a character's contracts</returns>
	IRequestClient<ContractBid[]> GetCorporationContractBids(long corporationId, long contractId, string token,
		int? page = null);

	/// <summary>
	///     Lists items of a particular contract
	///     <br /><br />
	///     This route is cached for an hour.
	///     <br /><br />
	///     This route is part of the rate limit group <c>corp-contract</c>. This group is limited to 600 tokens per 15
	///     minutes.
	///     <br /><br />
	///     This route requires the following scope: <c>esi-contracts.read_corporation_contracts.v1</c>.
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdContracts" />
	/// </remarks>
	/// <param name="corporationId">The ID of the corporation</param>
	/// <param name="contractId">ID of a contract</param>
	/// <param name="token">Access Token for the character</param>
	/// <returns>A Request client to get a character's contracts</returns>
	IRequestClient<ContractItem[]> GetCorporationContractItems(long corporationId, long contractId, string token);
}
