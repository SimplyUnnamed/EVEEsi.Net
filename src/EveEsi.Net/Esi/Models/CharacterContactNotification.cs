using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record CharacterContactNotification
{
	[JsonPropertyName("message")] public string Message { get; init; } = null!;

	[JsonPropertyName("notification_id")] public long NotificationId { get; init; }

	[JsonPropertyName("send_date")] public DateTime SendDate { get; init; }

	[JsonPropertyName("send_character_id")]
	public long SendCharacterId { get; init; }

	[JsonPropertyName("standing_level")] public double StandingLevel { get; init; }
}
