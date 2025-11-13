using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdNotificationsGet" />
/// </summary>
public record CharacterNotification
{
	[JsonPropertyName("is_read")] public bool IsRead { get; init; }

	[JsonPropertyName("notification_id")] public long NotificationId { get; init; }

	[JsonPropertyName("sender_id")] public long SenderId { get; init; }

	[JsonPropertyName("text")] public string Text { get; init; } = null!;

	[JsonPropertyName("timestamp")] public DateTime Timestamp { get; init; }

	[JsonPropertyName("sender_type")] public NotificationSenderType SenderType { get; init; }

	[JsonPropertyName("type")] public CharacterNotificationType NotificationType { get; init; }
}
