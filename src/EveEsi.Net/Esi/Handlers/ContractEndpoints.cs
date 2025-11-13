using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Factories;
using Endpoints = EveEsi.Net.ESI.Endpoints.Contracts;

namespace EveEsi.Net.Handlers;

internal class ContractEndpoints(IEsiRequestClientFactory clientFactory) : IContractEndpoints
{
	/// <inheritdoc />
	public IRequestClient<CharacterContract[]> GetCharacterContracts(long characterId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CharacterContract[]>(Endpoints.CharacterContracts, b =>
		{
			b.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<ContractBid[]> GetContractBids(long characterId, long contractId, string token)
	{
		return clientFactory.CreateClient<ContractBid[]>(Endpoints.CharacterContractBids, b =>
		{
			b.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			b.Route[ESI.Parameters.Route.ContractId] = contractId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<ContractItem[]> GetContractItems(long characterId, long contractId, string token)
	{
		return clientFactory.CreateClient<ContractItem[]>(Endpoints.CharacterContractItems, b =>
		{
			b.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			b.Route[ESI.Parameters.Route.ContractId] = contractId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<PublicContract[]> GetPublicContracts(long regionId, int? page = null)
	{
		return clientFactory.CreateClient<PublicContract[]>(Endpoints.PublicContracts, b =>
		{
			b.Route[ESI.Parameters.Route.RegionId] = regionId.ToString();
			b.ApplyPage(page);
		});
	}

	/// <inheritdoc />
	public IRequestClient<PublicContractBid[]> GetPublicContractBids(long contactId, int? page = null)
	{
		return clientFactory.CreateClient<PublicContractBid[]>(Endpoints.PublicContractBids, b =>
		{
			b.Query[ESI.Parameters.Route.ContractId] = contactId.ToString();
			b.ApplyPage(page);
		});
	}

	/// <inheritdoc />
	public IRequestClient<PublicContractItem[]> GetPublicContractItems(long contractId, int? page = null)
	{
		return clientFactory.CreateClient<PublicContractItem[]>(Endpoints.PublicContractItems, b =>
		{
			b.Query[ESI.Parameters.Route.ContractId] = contractId.ToString();
			b.ApplyPage(page);
		});
	}

	/// <inheritdoc />
	public IRequestClient<CorporationContract[]> GetCorporationContracts(long corporationId, string token,
		int? page = null)
	{
		return clientFactory.CreateClient<CorporationContract[]>(Endpoints.CorporationContracts, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<ContractBid[]> GetCorporationContractBids(long corporationId, long contractId, string token,
		int? page = null)
	{
		return clientFactory.CreateClient<ContractBid[]>(Endpoints.CorporationContractBids, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.Route[ESI.Parameters.Route.ContractId] = contractId.ToString();
			b.ApplyPage(page);
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<ContractItem[]> GetCorporationContractItems(long corporationId, long contractId, string token)
	{
		return clientFactory.CreateClient<ContractItem[]>(Endpoints.CorporationContractItems, b =>
		{
			b.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			b.Route[ESI.Parameters.Route.ContractId] = contractId.ToString();
		}, token);
	}
}
