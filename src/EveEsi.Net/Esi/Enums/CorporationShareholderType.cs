using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CorporationShareholderType
{
	
	[EnumMember(Value = "character")]Character,
	[EnumMember(Value = "corporation")]Corporation,
}
