using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

/// <summary>
///     See
///     <see href="https://developers.eveonline.com/api-explorer#/schemas/CharactersCharacterIdCorporationhistoryGet" />
/// </summary>
public record CharacterCorporationHistory
{
	[JsonPropertyName("corporation_id")] public required long CorporationId { get; init; }

	[JsonPropertyName("is_deleted")] public bool? IsDeleted { get; init; }

	[JsonPropertyName("record_id")] public required long RecordId { get; init; }

	[JsonPropertyName("start_date")] public required DateTime StartDate { get; init; }
}
