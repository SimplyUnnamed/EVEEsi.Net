using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CalendarOwnerType
{
	[EnumMember(Value = "eve_server")] EveServer,
	[EnumMember(Value = "corporation")] Corpoation,
	[EnumMember(Value = "faction")] Faction,
	[EnumMember(Value = "character")] Character,
	[EnumMember(Value = "alliance")] Alliance
}
