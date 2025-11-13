using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See
///     <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdCalendarGet">CharactersCharacterIdCalendarGet</see>
/// </summary>
public record CalendarItem
{
	[JsonPropertyName("event_date")] public DateTime? EventDate { get; init; }

	[JsonPropertyName("event_id")] public long? EventId { get; init; }

	[JsonPropertyName("event_response")] public CalendarEventResponse? EventResponse { get; init; }

	[JsonPropertyName("importance")] public long? Importance { get; init; }

	[JsonPropertyName("title")] public string? Title { get; init; }
}
