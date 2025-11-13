using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CalendarPutResponse
{
	[EnumMember(Value = "accepted")] Accepted,
	[EnumMember(Value = "declined")] Declined,
	[EnumMember(Value = "tentative")] Tentative
}
