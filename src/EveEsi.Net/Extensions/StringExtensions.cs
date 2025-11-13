using System.Security.Cryptography;
using System.Text;

namespace EveEsi.Net.Extensions;

public static class StringExtensions
{
	/// <summary>
	///     Checks if a string could be a JSON object or array.
	/// </summary>
	/// <param name="value">the string to check</param>
	public static bool IsPotentiallyJson(this string? value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return false;
		}

		return (value.StartsWith('{') && value.EndsWith('}')) || (value.StartsWith('[') && value.EndsWith(']'));
	}


	internal static string ToMd5Hash(this string value)
	{
		return Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(value)));
	}
}
