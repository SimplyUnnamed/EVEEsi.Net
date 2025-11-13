using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record CharacterJumpFatigue
{
	[JsonPropertyName("jump_fatigue_expire_date")]
	public DateTime JumpFatigueExpireDate { get; init; }

	[JsonPropertyName("last_jump_date")] public DateTime LastJumpDate { get; init; }

	[JsonPropertyName("last_update_date")] public DateTime LastUpdateDate { get; init; }
}
