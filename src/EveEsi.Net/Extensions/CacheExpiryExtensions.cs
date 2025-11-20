using EveEsi.Net.Config;

namespace EveEsi.Net.Extensions;

public static class CacheExpiryExtensions
{
	/// <summary>
	/// Calculate the TimeSpan to the next Expiry Date
	/// </summary>
	/// <param name="cacheExpiry">The Expiry Type</param>
	/// <returns>TimeSpan to the next Expiry</returns>
	/// <exception cref="NotSupportedException">
	///	Thrown if the CacheExpiry is an unsupported extension
	/// </exception>
	public static TimeSpan CalculateExpiry(this CacheExpiry cacheExpiry)
	{
		return cacheExpiry switch
		{
			TimeBasedExpiry timeBased => timeBased.Expiry,
			DailyExpiry daily => daily.Expiry.ToTimeSpan(),
			_ => throw new NotSupportedException("Unknown Expiry Type")
		};
	}
	
	/// <summary>
	/// Get the amount of time until the next Targeted TimeOnly.
	/// </summary>
	/// <param name="target">The Targeted the TimeOnly</param>
	/// <returns>The amount of Time till the next UTC TimeOnly Target</returns>
	private static TimeSpan GetTimeSpanFromTimeOnly(this TimeOnly target)
	{
		TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.UtcNow);
		TimeSpan timeDifference = target - currentTime;
		if (timeDifference.TotalHours < 0)
		{
			timeDifference = timeDifference.Add(TimeSpan.FromHours(24));
		}
		return timeDifference;
	}
}
