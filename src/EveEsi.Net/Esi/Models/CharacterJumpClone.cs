using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdClonesGet" />
/// </summary>
public record CharacterJumpClone
{
	[JsonPropertyName("implants")] public long[] Implants { get; init; } = [];

	[JsonPropertyName("jump_clone_id")] public long JumpCloneId { get; init; }

	[JsonPropertyName("location_id")] public long LocationId { get; init; }

	[JsonPropertyName("location_type")] public CloneLocationType LocationType { get; init; }

	[JsonPropertyName("name")] public string Name { get; init; } = null!;
}
