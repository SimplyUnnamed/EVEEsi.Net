using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ContractAvailability
{
	[EnumMember(Value = "public")] Public,
	[EnumMember(Value = "personal")] Personal,
	[EnumMember(Value = "corporation")] Corporation,
	[EnumMember(Value = "alliance")] Alliance
}
