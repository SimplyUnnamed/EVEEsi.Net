using System.Text.Json.Serialization;

namespace EveEsi.Net.Esi.Models;

public record CharacterAgentResearch
{
	[JsonPropertyName("agent_id")] public required long AgentId { get; init; }

	[JsonPropertyName("points_per_day")] public required double PointsPerDay { get; init; }

	[JsonPropertyName("remainder_points")] public required double RemainderPoints { get; init; }

	[JsonPropertyName("skill_type_id")] public required long SkillTypeId { get; init; }

	[JsonPropertyName("started_at")] public required DateTime StartedAt { get; init; }
}
