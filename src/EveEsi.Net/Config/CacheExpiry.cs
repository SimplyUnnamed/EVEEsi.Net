namespace EveEsi.Net.Config;

public abstract record CacheExpiry();

public sealed record TimeBasedExpiry(TimeSpan Expiry) : CacheExpiry;
public sealed record DailyExpiry(TimeOnly Expiry) : CacheExpiry;
public sealed record NotCached : CacheExpiry;
