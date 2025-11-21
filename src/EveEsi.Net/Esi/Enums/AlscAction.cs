using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum AlscAction
{
	[EnumMember(Value = "add")]
	Add,
	[EnumMember(Value = "assemble")]
	Assemble,
	[EnumMember(Value = "configure")]
	Configure,
	[EnumMember(Value = "enter_password")]
	EnterPassword,
	[EnumMember(Value = "lock")]
	Lock,
	[EnumMember(Value = "move")]
	Move,
	[EnumMember(Value = "repackage")]
	Repackage,
	[EnumMember(Value = "set_name")]
	SetName,
	[EnumMember(Value = "set_password")]
	SetPassword,
	[EnumMember(Value = "unlock")
	]Unlock
}
