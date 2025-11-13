using System.Collections.Concurrent;

namespace EveEsi.Net.Middleware;

internal sealed class AsyncLockPool : IDisposable
{
	private readonly Timer _cleanupTimer;

	private readonly ConcurrentDictionary<string, Entry> _map = new();
	private readonly TimeSpan _retention;
	private bool _disposed;

	/// <summary>
	///     retention: how long to keep unused semaphores before disposing them (recommended: 1-10 minutes).
	///     cleanupInterval: how frequently cleanup runs.
	/// </summary>
	public AsyncLockPool(TimeSpan? retention = null, TimeSpan? cleanupInterval = null)
	{
		_retention = retention ?? TimeSpan.FromMinutes(5);
		TimeSpan interval = cleanupInterval ?? TimeSpan.FromMinutes(1);
		_cleanupTimer = new Timer(_ => Cleanup(), null, interval, interval);
	}

	public void Dispose()
	{
		if (_disposed)
		{
			return;
		}

		_disposed = true;
		_cleanupTimer.Dispose();

		foreach (KeyValuePair<string, Entry> kv in _map)
		{
			try { kv.Value.Semaphore.Dispose(); }
			catch { }
		}

		_map.Clear();
	}

	/// <summary>
	///     Acquire the lock for a given key asynchronously.
	///     Returns an IDisposable that will Release the semaphore when disposed.
	///     Usage increments before waiting, and decrements after releasing.
	/// </summary>
	public async Task<IDisposable> AcquireAsync(string key)
	{
		if (_disposed)
		{
			throw new ObjectDisposedException(nameof(AsyncLockPool));
		}

		if (key is null)
		{
			throw new ArgumentNullException(nameof(key));
		}

		Entry entry = _map.GetOrAdd(key, _ => new Entry());

		// Indicate we want to use this entry so cleanup won't remove it.
		Interlocked.Increment(ref entry.Usage);

		try
		{
			// Wait for the semaphore to be available.
			await entry.Semaphore.WaitAsync().ConfigureAwait(false);

			// Now we hold the lock. Return a releaser that will release and update LastUsed/Usage.
			return new Releaser(this, key, entry);
		}
		catch
		{
			// If WaitAsync throws, decrement usage we incremented earlier.
			Interlocked.Decrement(ref entry.Usage);
			throw;
		}
	}

	private void Release(string key, Entry entry)
	{
		// Release the semaphore then update usage and LastUsed.
		try
		{
			entry.Semaphore.Release();
		}
		finally
		{
			entry.LastUsed = DateTimeOffset.UtcNow;
			Interlocked.Decrement(ref entry.Usage);
		}
	}

	private void Cleanup()
	{
		if (_disposed)
		{
			return;
		}

		DateTimeOffset cutoff = DateTimeOffset.UtcNow - _retention;

		foreach (KeyValuePair<string, Entry> kv in _map)
		{
			string key = kv.Key;
			Entry entry = kv.Value;

			// only consider entries not currently in use
			if (Volatile.Read(ref entry.Usage) != 0)
			{
				continue;
			}

			if (entry.LastUsed <= cutoff)
			{
				// Try to remove; only remove if the same instance is present (TryRemove overload).
				if (_map.TryRemove(new KeyValuePair<string, Entry>(key, entry)))
				{
					// Dispose the semaphore safely
					try { entry.Semaphore.Dispose(); }
					catch
					{
						/* swallow */
					}
				}
			}
		}
	}

	private sealed class Entry
	{
		public readonly SemaphoreSlim Semaphore = new(1, 1);

		// Most recent time when the lock was used or released.
		public DateTimeOffset LastUsed = DateTimeOffset.UtcNow;

		// Number of callers that have acquired (and not yet released) this lock.
		public int Usage;
	}

	private sealed class Releaser : IDisposable
	{
		private readonly string _key;
		private readonly AsyncLockPool _pool;
		private Entry? _entry;

		public Releaser(AsyncLockPool pool, string key, Entry entry)
		{
			_pool = pool;
			_key = key;
			_entry = entry;
		}

		public void Dispose()
		{
			Entry? e = Interlocked.Exchange(ref _entry, null);
			if (e is not null)
			{
				_pool.Release(_key, e);
			}
		}
	}
}
