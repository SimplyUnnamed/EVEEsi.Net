using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationStarbaseInfo
{
	public sealed record FuelBayItem
	{
		[JsonPropertyName("quantity")]
		public long Quantity { get; init; }
		[JsonPropertyName("type_id")]
		public long TypeId { get; init; }
	}
	
	[JsonConverter(typeof(JsonStringEnumMemberConverter))]
	public enum StarbaseRole
	{
		[EnumMember(Value = "alliance_member")]AllianceMember,
		[EnumMember(Value = "config_starbase_equipment_role")]ConfigStarbaseEquipmentRoles,
		[EnumMember(Value = "corporation_member")]CorporationMember,
		[EnumMember(Value = "starbase_fuel_technician_role")]StarbaseFuelTechnicianRole,
	}

	
	[JsonPropertyName("allow_alliance_members")]
	public bool AllowAllianceMembers { get; init; }
	
	[JsonPropertyName("allow_corporation_members")]
	public bool AllowCorporationMembers { get; init; }
	
	/// <summary>
	/// Who can anchor starbase (POS) and its structures 
	/// </summary>
	[JsonPropertyName("anchor")]
	public StarbaseRole Anchor { get; init; }
	
	[JsonPropertyName("attack_if_at_war")]
	public bool AttachIfAtWar { get; init; }
	
	/// <summary>
	/// Starbase (POS) will attack if target's security standing is lower than this value
	/// </summary>
	[JsonPropertyName("attack_if_other_security_status_dropping")]
	public bool AttackIfOtherSecurityStatusDropping { get; init; }
	
	/// <summary>
	/// Starbase (POS) will attack if target's standing is lower than this value
	/// </summary>
	[JsonPropertyName("attack_security_status_threshold")]
	public double AttackSecurityStatusThreshold { get; init; }
	
	[JsonPropertyName("attack_standing_threshold")]
	public double AttackStandingThreshold { get; init; }
	
	/// <summary>
	/// Who can take fuel blocks out of the starbase (POS)'s fuel bay
	/// </summary>
	[JsonPropertyName("fuel_bay_take")]
	public StarbaseRole FieldBayTake { get; init; }
	
	/// <summary>
	/// Who can view the starbase (POS)'s fuel bay. Characters either need to have required role or belong to the
	/// starbase (POS) owner's corporation or alliance, as described by the enum, all other access settings follows
	/// the same scheme
	/// </summary>
	[JsonPropertyName("fuel_bay_view")]
	public StarbaseRole FuelBayView { get; init; }

	/// <summary>
	/// Fuel blocks and other things that will be consumed when operating a starbase (POS)
	/// </summary>
	[JsonPropertyName("fuels")] 
	public FuelBayItem[] Fuels { get; init; } = [];
	
	/// <summary>
	/// Who can offline starbase (POS) and its structures
	/// </summary>
	[JsonPropertyName("offline")] 
	public StarbaseRole Offline { get; init; }
	
	/// <summary>
	/// Who can online starbase (POS) and its structures
	/// </summary>
	[JsonPropertyName("online")]
	public StarbaseRole Online { get; init; }
	
	/// <summary>
	/// Who can unanchor starbase (POS) and its structures
	/// </summary>
	[JsonPropertyName("unanchor")]
	public StarbaseRole Unanchor { get; init; }
	
	/// <summary>
	/// True if the starbase (POS) is using alliance standings, otherwise using corporation's
	/// </summary>
	[JsonPropertyName("use_alliance_standings")]
	public bool UseAllianceStanding { get; init; }
}
