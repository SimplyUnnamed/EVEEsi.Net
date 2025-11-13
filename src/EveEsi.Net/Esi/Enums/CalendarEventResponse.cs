using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CalendarEventResponse
{
	[EnumMember(Value = "declined")] Declined,
	[EnumMember(Value = "not_responded")] NotResponded,
	[EnumMember(Value = "accepted")] Accepted,
	[EnumMember(Value = "tentative")] Tentative
}
