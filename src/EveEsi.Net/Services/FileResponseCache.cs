using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using EveEsi.Net.Middleware;

namespace EveEsi.Net.Services;

public class FileResponseCache : IEsiCacheService, IDisposable
{
	private readonly string _folder;
	private readonly AsyncLockPool _lockPool;
	private bool _disposed;


	public FileResponseCache(string? folder = null)
	{
		_folder = Path.GetFullPath(folder ?? Path.Combine("eveesinet", "storage", "cache"));
		Directory.CreateDirectory(_folder);
		_lockPool = new AsyncLockPool(TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(1));
	}

	public void Dispose()
	{
		_lockPool.Dispose();
		_disposed = true;
	}

	public async Task<bool> StoreValue<T>(string esiRoute, T value, TimeSpan? ttl = null)
	{
		if (_disposed)
		{
			throw new ObjectDisposedException(nameof(FileResponseCache));
		}

		ArgumentNullException.ThrowIfNull(esiRoute);

		TimeSpan lifetime = ttl ?? TimeSpan.FromHours(1);
		DateTimeOffset expiry = DateTimeOffset.UtcNow.Add(lifetime);
		EsiCacheItem<T> cacheItem = new(esiRoute, value, expiry);

		string filePath = GetPathForKey(esiRoute);

		using (await _lockPool.AcquireAsync(filePath))
		{
			string tempPath = filePath + ".tmp";
			try
			{
				using (FileStream fs = new(tempPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096,
					       true))
				{
					await JsonSerializer.SerializeAsync(fs, cacheItem);
					await fs.FlushAsync();
				}

				;

				File.Move(tempPath, filePath, true);
				return true;
			}
			catch
			{
				if (File.Exists(tempPath))
				{
					TryRun(() => File.Delete(tempPath));
				}
			}
		}

		return false;
	}

	public async Task<T?> GetValue<T>(string key)
	{
		if (_disposed)
		{
			throw new ObjectDisposedException(nameof(FileResponseCache));
		}

		ArgumentNullException.ThrowIfNull(key);

		string filePath = GetPathForKey(key);
		if (!File.Exists(filePath))
		{
			return default;
		}

		using (await _lockPool.AcquireAsync(filePath))
		{
			if (!File.Exists(filePath))
			{
				return default;
			}

			EsiCacheItem<T>? cacheItem;
			try
			{
				await using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
					true);
				cacheItem = await JsonSerializer.DeserializeAsync<EsiCacheItem<T>>(fs);
			}
			catch
			{
				TryRun(() =>
				{
					File.Delete(filePath);
				});
				return default;
			}

			if (cacheItem == null)
			{
				TryRun(() =>
				{
					File.Delete(filePath);
				});
				return default;
			}

			if (cacheItem.Endpoint != key)
			{
				TryRun(() =>
				{
					File.Delete(filePath);
				});
				return default;
			}

			if (cacheItem.UtcExpiry <= DateTimeOffset.UtcNow)
			{
				TryRun(() =>
				{
					File.Delete(filePath);
				});
				return default;
			}

			return cacheItem.Data;
		}
	}

	private string GetPathForKey(string esiRoute)
	{
		// Use SHA256 of the key to make safe, short filenames
		using SHA256 sha = SHA256.Create();
		byte[] bytes = Encoding.UTF8.GetBytes(esiRoute);
		byte[] hash = sha.ComputeHash(bytes);
		string hex = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
		return Path.Combine(_folder, $"{hex}.cachejson");
	}


	private void TryRun(Action run)
	{
		try
		{
			run();
		}
		catch { }
	}


	private string CreateKeyPath(string key)
	{
		return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage", "cache", key);
	}

	private sealed class EsiCacheItem<T>
	{
		public EsiCacheItem(string endpoint, T data, DateTimeOffset utcExpiry)
		{
			Endpoint = endpoint;
			Data = data;
			UtcExpiry = utcExpiry;
		}

		public string Endpoint { get; set; }

		public T Data { get; init; }

		public DateTimeOffset UtcExpiry { get; init; }
	}
}
