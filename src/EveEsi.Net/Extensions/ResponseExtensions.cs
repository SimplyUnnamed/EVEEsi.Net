using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Extensions;

public static class ResponseExtensions
{
	/// <summary>
	///     Parses the EsiResponseContext into its response model.
	/// </summary>
	/// <param name="ctx">the EsiResponseContext</param>
	/// <typeparam name="T">The data model to return</typeparam>
	/// <returns>returns the represented data in the response.</returns>
	public static T GetValue<T>(this EsiResponse<T> ctx)
	{
		if (ctx.TryGetData(out T? value, out string? error))
		{
			return value;
		}

		throw new InvalidOperationException(error);
	}
}
