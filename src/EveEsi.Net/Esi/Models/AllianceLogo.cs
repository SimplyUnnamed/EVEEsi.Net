using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See
///     <see href="https://developers.eveonline.com/api-explorer#/schemas/AlliancesAllianceIdIconsGet">AlliancesAllianceIdIconsGet</see>
/// </summary>
public record AllianceLogo
{
	[JsonPropertyName("px128x128")] public string X128 { get; init; } = null!;

	[JsonPropertyName("px64x64")] public string X64 { get; init; } = null!;
}
