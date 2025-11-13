using System.Runtime.Serialization;

namespace EveEsi.Net.Esi.Enums;

public enum ContractAvailability
{
	[EnumMember(Value = "public")] Public,
	[EnumMember(Value = "personal")] Personal,
	[EnumMember(Value = "corporation")] Corporation,
	[EnumMember(Value = "alliance")] Alliance
}
