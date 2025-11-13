using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdRolesGet" />
/// </summary>
public record CharacterCorporationRoles
{
	[JsonPropertyName("roles")] public CorporationRoles[] Roles { get; init; } = [];

	[JsonPropertyName("roles_at_base")] public CorporationRoles[] RolesAtBase { get; init; } = [];

	[JsonPropertyName("roles_at_hq")] public CorporationRoles[] RolesAtHq { get; init; } = [];

	[JsonPropertyName("roles_at_other")] public CorporationRoles[] RolesAtOther { get; init; } = [];
}
