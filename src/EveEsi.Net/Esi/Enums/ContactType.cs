using System.Runtime.Serialization;

namespace EveEsi.Net.Esi.Enums;

public enum ContactType
{
	[EnumMember(Value = "character")] Character,
	[EnumMember(Value = "corporation")] Corporation,
	[EnumMember(Value = "alliance")] Alliance,
	[EnumMember(Value = "faction")] Faction
}
