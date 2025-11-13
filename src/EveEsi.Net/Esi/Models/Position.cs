using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record Position
{
	[JsonPropertyName("x")] public required double X { get; init; }

	[JsonPropertyName("y")] public required double Y { get; init; }

	[JsonPropertyName("z")] public required double Z { get; init; }
}
