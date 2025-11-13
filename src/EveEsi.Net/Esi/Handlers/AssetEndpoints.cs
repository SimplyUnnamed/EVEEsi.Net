using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;

namespace EveEsi.Net.Handlers;

internal class AssetEndpoints(IEsiRequestClientFactory clientFactory) : IAssetsEndpoints
{
	/// <inheritdoc />
	public IRequestClient<CharacterAsset[]> GetCharacterAssets(long characterId, string token, int? page = 1)
	{
		return clientFactory.CreateClient<CharacterAsset[]>(ESI.Endpoints.Assets.CharacterAssetList, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Query[ESI.Parameters.Query.Page] = page?.ToString() ?? "1";
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<AssetLocation[]> GetCharacterAssetLocations(long characterId, long[] assetId, string token)
	{
		return clientFactory.CreateClient<AssetLocation[]>(ESI.Endpoints.Assets.CharacterAssetLocations, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Body = assetId;
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<AssetName[]> GetCharacterAssetNames(long characterId, long[] assetId, string token)
	{
		return clientFactory.CreateClient<AssetName[]>(ESI.Endpoints.Assets.CharacterAssetNames, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Body = assetId;
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationAsset[]> GetCorporationAssets(long corporationId, string token, int? page = 1)
	{
		return clientFactory.CreateClient<CorporationAsset[]>(ESI.Endpoints.Assets.CorporationAssetList, p =>
		{
			p.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			p.Query[ESI.Parameters.Query.Page] = page?.ToString() ?? "1";
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<AssetLocation[]> GetCorporationAssetLocations(long corporationId, long[] assetId,
		string token)
	{
		return clientFactory.CreateClient<AssetLocation[]>(ESI.Endpoints.Assets.CorporationAssetLocations, p =>
		{
			p.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			p.Body = assetId;
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<AssetName[]> GetCorporationAssetNames(long corporationId, long[] assetId, string token)
	{
		return clientFactory.CreateClient<AssetName[]>(ESI.Endpoints.Assets.CorporationAssetNames, p =>
		{
			p.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
			p.Body = assetId;
		}, token);
	}
}
