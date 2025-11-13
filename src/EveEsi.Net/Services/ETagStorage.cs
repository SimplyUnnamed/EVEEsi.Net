using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace EveEsi.Net.Services;

internal class ETagStorage : IEtagStorage
{
	private readonly ConcurrentDictionary<string, string> _etags = new();

	public Task<bool> TryGetETagAsync(string etagKey, [MaybeNullWhen(false)] out string? etag)
	{
		etag = null;
		if (_etags.TryGetValue(etagKey, out string? storedTag))
		{
			etag = storedTag;
		}

		return Task.FromResult(etag != null);
	}

	public Task StoreEtagAsync(string etagKey, string etag)
	{
		if (_etags.ContainsKey(etagKey))
		{
			_etags.TryRemove(etagKey, out _);
		}

		_etags.TryAdd(etagKey, etag);
		return Task.CompletedTask;
	}
}
