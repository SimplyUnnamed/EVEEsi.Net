using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface IAssetsEndpoints
{
	/// <summary>
	///     Return a list of the characters assets
	/// </summary>
	/// <remarks>
	///     The endpoint is paginated.
	///     For the total number of pages, see <see cref="EsiResponse" /> 'X-Pages' response header
	/// </remarks>
	/// <param name="characterId">Id of the character</param>
	/// <param name="token">AccessToken of the character</param>
	/// <param name="page">The page to get</param>
	/// <returns>List of Assets</returns>
	public IRequestClient<CharacterAsset[]> GetCharacterAssets(long characterId, string token, int? page = 1);

	/// <summary>
	///     Return locations for a set of item ids, which you can get from character assets endpoint. Coordinates for items in
	///     hangars or stations are set to (0,0,0)
	/// </summary>
	/// <param name="characterId">Id of the character</param>
	/// <param name="assetId">list of asset id's</param>
	/// <param name="token">AccessToken of the character</param>
	/// <returns>List of Asset Locations</returns>
	public IRequestClient<AssetLocation[]> GetCharacterAssetLocations(long characterId, long[] assetId, string token);

	/// <summary>
	///     Return names for a set of item ids, which you can get from character assets endpoint.
	/// </summary>
	/// <param name="characterId">Id of the character</param>
	/// <param name="assetId">list of asset id's</param>
	/// <param name="token">AccessToken of the character</param>
	/// <returns>List of Asset Names</returns>
	public IRequestClient<AssetName[]> GetCharacterAssetNames(long characterId, long[] assetId, string token);

	/// <summary>
	///     Return a list of the corporations assets
	/// </summary>
	/// <remarks>
	///     The endpoint is paginated.
	///     For the total number of pages, see <see cref="EsiResponse" /> 'X-Pages' response header
	/// </remarks>
	/// <param name="corporationId">Id of the corporation</param>
	/// <param name="token">AccessToken of the character</param>
	/// <param name="page">The page to get</param>
	/// <returns>List of Assets</returns>
	public IRequestClient<CorporationAsset[]> GetCorporationAssets(long corporationId, string token, int? page = 1);

	/// <summary>
	///     Return locations for a set of item ids, which you can get from character assets endpoint. Coordinates for items in
	///     hangars or stations are set to (0,0,0)
	/// </summary>
	/// <param name="corporationId">Id of the corporation</param>
	/// <param name="assetId">list of asset id's</param>
	/// <param name="token">AccessToken of the character</param>
	/// <returns>List of Asset Locations</returns>
	public IRequestClient<AssetLocation[]> GetCorporationAssetLocations(long corporationId, long[] assetId,
		string token);

	/// <summary>
	///     Return names for a set of item ids, which you can get from character assets endpoint.
	/// </summary>
	/// <param name="corporationId">Id of the corporation</param>
	/// <param name="assetId">list of asset id's</param>
	/// <param name="token">AccessToken of the character</param>
	/// <returns>List of Asset Names</returns>
	public IRequestClient<AssetName[]> GetCorporationAssetNames(long corporationId, long[] assetId, string token);
}
