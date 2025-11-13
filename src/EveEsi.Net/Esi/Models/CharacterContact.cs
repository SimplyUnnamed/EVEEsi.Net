using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public record CharacterContact
{
	[JsonPropertyName("contact_id")] public long ContactId { get; init; }

	[JsonPropertyName("contact_type")] public ContactType ContactType { get; init; }

	[JsonPropertyName("is_blocked")] public bool IsBlocked { get; init; }

	[JsonPropertyName("is_watched")] public bool IsWanted { get; init; }

	[JsonPropertyName("label_ids")] public long[] LabelIds { get; init; } = [];

	[JsonPropertyName("standings")] public double Standing { get; init; }
}
