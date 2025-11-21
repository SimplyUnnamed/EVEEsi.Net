using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CorporationRoleType
{
	[EnumMember(Value = "grantable_roles")]
	GrantableRoles,
	[EnumMember(Value = "grantable_roles_at_base")]
	GrantableRolesAtBase,
	[EnumMember(Value = "grantable_roles_at_hq")]
	GrantableRolesAtHq,
	[EnumMember(Value = "grantable_roles_at_other")]
	GrantableRolesAtOther,
	[EnumMember(Value = "roles")]
	Roles,
	[EnumMember(Value = "roles_at_base")]
	RolesAtBase,
	[EnumMember(Value = "roles_at_hq")]
	RolesAtHq,
	[EnumMember(Value = "roles_at_other")]
	RolesAtOther,
}
