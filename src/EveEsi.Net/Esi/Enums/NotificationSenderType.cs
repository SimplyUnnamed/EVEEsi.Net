using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum NotificationSenderType
{
	[EnumMember(Value = "character")] Character,
	[EnumMember(Value = "corporation")] Corporation,
	[EnumMember(Value = "alliance")] Alliance,
	[EnumMember(Value = "faction")] Faction,
	[EnumMember(Value = "other")] Other
}
