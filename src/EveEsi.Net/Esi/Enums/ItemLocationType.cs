using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ItemLocationType
{
	[EnumMember(Value = "station")] Station,
	[EnumMember(Value = "solar_system")] SolarSystem,
	[EnumMember(Value = "item")] Item,
	[EnumMember(Value = "other")] Other
}
