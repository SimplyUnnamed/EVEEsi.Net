using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdClonesGet" />
/// </summary>
public record CharacterCloneDetails
{
	[JsonPropertyName("home_location_id")] public CharacterCloneHome HomeLocation { get; init; } = null!;

	[JsonPropertyName("jump_clones")] public CharacterJumpClone[] JumpClones { get; init; } = [];

	[JsonPropertyName("last_clone_jump_date")]
	public DateTime LastCloneJump { get; init; }

	[JsonPropertyName("last_station_change_date")]
	public DateTime LastStationChange { get; init; }
}
