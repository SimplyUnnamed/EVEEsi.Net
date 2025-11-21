using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ContractType
{
	[EnumMember(Value = "unknown")] Unknown,
	[EnumMember(Value = "item_exchange")] ItemExchange,
	[EnumMember(Value = "auction")] Auction,
	[EnumMember(Value = "courier")] Courier,
	[EnumMember(Value = "loan")] Loan
}
