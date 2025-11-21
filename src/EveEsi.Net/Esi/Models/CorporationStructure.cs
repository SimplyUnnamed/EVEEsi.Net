using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationStructure
{
	public sealed record StructureService
	{

		[JsonPropertyName("name")]
		public string Name { get; init; } = null!;
		[JsonPropertyName("state")]
		public StructureServiceState State { get; init; }
		
	}
	
	[JsonPropertyName("corporation_id")]
	public long CorporationId { get; init; }
	
	[JsonPropertyName("fuel_expires")]
	public DateTime? FuelExpires { get; init; }
	
	[JsonPropertyName("name")]
	public string Name { get; init; } = null!;
	
	[JsonPropertyName("next_reinforce_apply")]
	public DateTime? NextReinforceApply { get; init; }
	
	[JsonPropertyName("next_reinforce_hour")]
	public long? NextReinforceHour { get; init; }
	
	/// <summary>
	/// The id of the ACL profile for this citadel
	/// </summary>
	[JsonPropertyName("profile_id")]
	public long ProfileId { get; init; }

	/// <summary>
	/// The hour of day that determines the four hour window when the structure will randomly exit its reinforcement
	/// periods and become vulnerable to attack against its armor and/or hull. The structure will become vulnerable
	/// at a random time that is +/- 2 hours centered on the value of this property
	/// </summary>
	[JsonPropertyName("reinforce_hour")]
	public long ReinforceHour { get; init; }

	/// <summary>
	/// Contains a list of service upgrades, and their state
	/// </summary>
	[JsonPropertyName("services")]
	public StructureService[] Services { get; init; } = [];
	
	[JsonPropertyName("state")]
	public StructureState State { get; init; } = StructureState.Unknown;
	
	/// <summary>
	/// Date at which the structure will move to it's next state
	/// </summary>
	[JsonPropertyName("state_timer_end")]
	public DateTime? StartTimerEnd {get; init; }
	
	/// <summary>
	/// Date at which the structure entered it's current state
	/// </summary>
	[JsonPropertyName("state_timer_start")]
	public DateTime? StartTimerStart {get; init; }

	[JsonPropertyName("structure_id")]
	public long StructureId { get; init; }
	
	[JsonPropertyName("system_id")]
	public long SystemId { get; init; }
	
	[JsonPropertyName("type_id")]
	public long TypeId { get; init; }
	
	[JsonPropertyName("unanchors_at")]
	public DateTime? UnanchorsAt { get; init; }
}
