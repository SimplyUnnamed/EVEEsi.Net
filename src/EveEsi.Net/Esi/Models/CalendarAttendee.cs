using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public record CalendarAttendee
{
	[JsonPropertyName("character_id")] public long CharacterId { get; init; }

	[JsonPropertyName("event_response")] public CalendarEventResponse Response { get; init; }
}
