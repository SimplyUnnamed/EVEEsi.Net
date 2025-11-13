using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MedalStatus
{
	[EnumMember(Value = "public")] Public,
	[EnumMember(Value = "private")] Private
}
