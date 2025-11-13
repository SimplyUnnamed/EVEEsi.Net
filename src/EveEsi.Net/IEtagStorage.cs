using System.Diagnostics.CodeAnalysis;

namespace EveEsi.Net;

public interface IEtagStorage
{
	/// <summary>
	///     Try to get the cached ETag header value
	/// </summary>
	/// <param name="etagKey">storage key</param>
	/// <param name="etag">Header Value</param>
	/// <returns>Boolean indicating a cache hit.</returns>
	Task<bool> TryGetETagAsync(string etagKey, [MaybeNullWhen(false)] out string? etag);

	/// <summary>
	///     Stores an ETag header in the cache
	/// </summary>
	/// <param name="etagKey">storage key</param>
	/// <param name="etag">etag value</param>
	Task StoreEtagAsync(string etagKey, string etag);
}
