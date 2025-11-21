using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum AlscPasswordType
{
	[EnumMember(Value = "config")]
	Config,
	[EnumMember(Value = "general")]
	General
}
