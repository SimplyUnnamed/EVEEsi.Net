using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum StarbaseState
{
	[EnumMember(Value = "offline")]
	Offline,
	[EnumMember(Value = "online")]
	Online,
	[EnumMember(Value = "onlining")]
	Onlining,
	[EnumMember(Value = "reinforced")]
	Reinforcing,
	[EnumMember(Value = "unanchoring")]
	Unachoring,
}
