using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public record CalendarEvent
{
	[JsonPropertyName("date")] public required DateTime Date { get; init; }

	/// <summary>
	///     Length in minutes
	/// </summary>
	[JsonPropertyName("duration")]
	public required long Duration { get; init; }

	[JsonPropertyName("event_id")] public required long EventId { get; init; }

	[JsonPropertyName("importance")] public required long Importance { get; init; }

	[JsonPropertyName("owner_id")] public required long OwnerId { get; init; }

	[JsonPropertyName("owner_name")] public required long OwnerName { get; init; }

	[JsonPropertyName("owner_type")] public required CalendarOwnerType OwnerType { get; init; }

	[JsonPropertyName("response")] public required string Response { get; init; }

	[JsonPropertyName("text")] public required string Text { get; init; }

	[JsonPropertyName("title")] public required string Title { get; init; }
}
