using EveEsi.Net.Esi.Models;
using EveEsi.Net.Esi.Requests;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;

namespace EveEsi.Net.Handlers;

internal class CalendarEndpoints(IEsiRequestClientFactory clientFactory) : ICalendarEndpoints
{
	/// <inheritdoc />
	public IRequestClient<CalendarItem[]> GetCalendarEvents(long characterId, string token, long? fromEvent = null)
	{
		return clientFactory.CreateClient<CalendarItem[]>(ESI.Endpoints.Calendar.CalendarItems, p =>
			{
				p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
				if (fromEvent != null)
				{
					p.Query[ESI.Parameters.Query.FromEvent] = fromEvent.ToString();
				}
			},
			token);
	}

	/// <inheritdoc />
	public IRequestClient<CalendarEvent> GetCalendarEvent(long characterId, long eventId, string token)
	{
		return clientFactory.CreateClient<CalendarEvent>(ESI.Endpoints.Calendar.CalendarEvent, p =>
			{
				p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
				p.Route[ESI.Parameters.Route.EventId] = eventId.ToString();
			},
			token);
	}

	/// <inheritdoc />
	public IRequestClient ResponseToEvent(long characterId, long eventId, CalendarResponseRequest response,
		string token)
	{
		return clientFactory.CreateClient(ESI.Endpoints.Calendar.RespondToEvent, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Route[ESI.Parameters.Route.EventId] = eventId.ToString();
			p.Body = response;
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CalendarAttendee[]> GetCalendarAttendees(long characterId, long eventId, string token)
	{
		return clientFactory.CreateClient<CalendarAttendee[]>(ESI.Endpoints.Calendar.EventAttendees,
			p =>
			{
				p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
				p.Route[ESI.Parameters.Route.EventId] = eventId.ToString();
			}, token);
	}
}
