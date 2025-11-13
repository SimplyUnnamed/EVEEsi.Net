using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Requests;

/// <summary>
///     Request body for Response to Calendar Event.
///     <see href="https://developers.eveonline.com/api-explorer#/operations/PutCharactersCharacterIdCalendarEventId" />
/// </summary>
public class CalendarResponseRequest
{
	[JsonPropertyName("response")] public CalendarPutResponse Response { get; init; }
}
