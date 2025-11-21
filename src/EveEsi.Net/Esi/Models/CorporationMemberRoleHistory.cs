using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationMemberRoleHistory
{
	[JsonPropertyName("changed_at")]
	public DateTime ChangedAt { get; init; }
	
	[JsonPropertyName("character_id")]
	public long CharacterId { get; init; }

	[JsonPropertyName("new_roles")]
	public CorporationRoles[] NewRoles { get; init; } = [];

	[JsonPropertyName("old_roles")]
	public CorporationRoles[] OldRoles { get; init; } = [];
	
	[JsonPropertyName("tole_type")]
	public CorporationRoleType RoleType { get; init; }
}
