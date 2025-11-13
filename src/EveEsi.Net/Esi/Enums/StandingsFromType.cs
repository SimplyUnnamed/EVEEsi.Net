using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum StandingsFromType
{
	[EnumMember(Value = "agent")] Agent,
	[EnumMember(Value = "npc_corp")] NpcCorp,
	[EnumMember(Value = "faction")] Faction
}
