using EveEsi.Net.Esi.Models;
using EveEsi.Net.Esi.Requests;
using EveEsi.Net.EsiClient;

namespace EveEsi.Net;

public interface ICalendarEndpoints
{
	/// <summary>
	///     Get 50 event summaries from the calendar. If no from_event ID is given, the resource will return the next 50
	///     chronological event summaries from now. If a fromEvent ID is specified, it will return the next 50 chronological
	///     event summaries from after that event
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdCalendar" />
	/// </remarks>
	/// <param name="characterId">ID of the Character</param>
	/// <param name="token">Access Token of the character</param>
	/// <param name="fromEvent">ID of the Event to fetch from</param>
	/// <returns>List of Calendar Events</returns>
	public IRequestClient<CalendarItem[]> GetCalendarEvents(long characterId, string token, long? fromEvent = null);

	/// <summary>
	///     Get all the information for a specific event
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdCalendarEventId" />
	/// </remarks>
	/// <param name="characterId">ID of the Character</param>
	/// <param name="eventId">ID of the calendar event</param>
	/// <param name="token">Access Token of the character</param>
	/// <returns>A Calendar Event</returns>
	public IRequestClient<CalendarEvent> GetCalendarEvent(long characterId, long eventId, string token);

	/// <summary>
	///     Set your response status to an event
	/// </summary>
	/// <remarks>
	///     <see href="https://developers.eveonline.com/api-explorer#/operations/PutCharactersCharacterIdCalendarEventId" />
	/// </remarks>
	/// <param name="characterId">ID of the Character</param>
	/// <param name="eventId">ID of the calendar event</param>
	/// <param name="response">Event Response body</param>
	/// <param name="token">Access Token of the character</param>
	/// <returns>A Calendar Event</returns>
	public IRequestClient ResponseToEvent(long characterId, long eventId, CalendarResponseRequest response,
		string token);

	/// <summary>
	///     Get all invited attendees for a given event
	/// </summary>
	/// <remarks>
	///     <see
	///         href="https://developers.eveonline.com/api-explorer#/operations/GetCharactersCharacterIdCalendarEventIdAttendees" />
	/// </remarks>
	/// <param name="characterId">ID of the Character</param>
	/// <param name="eventId">ID of the calendar event</param>
	/// <param name="token">Access Token of the character</param>
	/// <returns>A Calendar Event</returns>
	public IRequestClient<CalendarAttendee[]> GetCalendarAttendees(long characterId, long eventId, string token);
}
