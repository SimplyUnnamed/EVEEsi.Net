using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public record CorporationTitle
{
	[JsonPropertyName("title_id")]
	public long TitleId { get; init; }
	
	[JsonPropertyName("name")]
	public string? Name { get; init; }
	
	[JsonPropertyName("grantable_roles")] 
	public CorporationRoles[] GrantableRoles { get; init; } = [];
	
	[JsonPropertyName("grantable_roles_at_base")] 
	public CorporationRoles[] GrantableRolesAtBase { get; init; } = [];
	
	[JsonPropertyName("grantable_roles_at_hq")] 
	public CorporationRoles[] GrantableRolesAtHq { get; init; } = [];
	
	[JsonPropertyName("grantable_roles_at_other")] 
	public CorporationRoles[] GrantableRolesAtOther { get; init; } = [];
	
	[JsonPropertyName("roles")] 
	public CorporationRoles[] Roles { get; init; } = [];
	
	[JsonPropertyName("roles_at_base")] 
	public CorporationRoles[] RolesAtBase { get; init; } = [];

	[JsonPropertyName("roles_at_hq")] 
	public CorporationRoles[] RolesAtHq { get; init; } = [];
	
	[JsonPropertyName("roles_at_Other")] 
	public CorporationRoles[] RolesAtOther { get; init; } = [];
};
