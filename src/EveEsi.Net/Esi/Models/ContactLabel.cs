using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record ContactLabel
{
	[JsonPropertyName("label_id")] public long LabelId { get; init; }

	[JsonPropertyName("label_name")] public string LabelName { get; init; } = null!;
}
