using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CloneLocationType
{
	[EnumMember(Value = "station")] Station,
	[EnumMember(Value = "structure")] Structure
}
