namespace EveEsi.Net;

public interface IEsiCacheService
{
	Task<bool> StoreValue<T>(string esiRoute, T value, TimeSpan? ttl = null);
	Task<T?> GetValue<T>(string esiRoute);
}
