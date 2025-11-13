using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public record AllianceContact
{
	[JsonPropertyName("contact_id")] public long ContactId { get; init; }

	[JsonPropertyName("contact_type")] public ContactType ContactType { get; init; }

	[JsonPropertyName("label_ids")] public long[] LabelIds { get; init; } = [];

	[JsonPropertyName("standing")] public double Standing { get; init; }
}
