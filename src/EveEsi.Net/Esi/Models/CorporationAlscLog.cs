using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using EveEsi.Net.Esi.Enums;

namespace EveEsi.Net.Esi.Models;

public sealed record CorporationAlscLog
{
	[JsonPropertyName("action")]
	public AlscAction Action { get; init; }
	
	/// <summary>
	/// ID of the character who performed the action.
	/// </summary>
	[JsonPropertyName("character_id")]
	public long CharacterId { get; init; }
	
	/// <summary>
	/// ID of the container
	/// </summary>
	[JsonPropertyName("container_id")]
	public long ContainerId { get; init; }
	
	/// <summary>
	/// Type ID of the container
	/// </summary>
	[JsonPropertyName("container_type_id")]
	public long ContainerTypeId { get; init; }
	
	[JsonPropertyName("location_flag")]
	public CorporationItemLocationFlag LocationFlag { get; init; }
	
	[JsonPropertyName("location_id")]
	public long LocationId { get; init; }
	
	/// <summary>
	/// Timestamp when this log was created
	/// </summary>
	[JsonPropertyName("logged_at")]
	public DateTime LoggedAt { get; init; }
	
	[JsonPropertyName("new_config_bitmask")]
	public long? NewConfigBitMask { get; init; }
	
	[JsonPropertyName("old_config_bitmask")]
	public long? OldConfigBitMask { get; init; }
	
	/// <summary>
	/// Type of password set if action is of type SetPassword or EnterPassword
	/// </summary>
	[JsonPropertyName("password_type")]
	public AlscPasswordType? PasswordType { get; init; }
	
	/// <summary>
	/// Quantity of the item being acted upon
	/// </summary>
	[JsonPropertyName("quantity")]
	public long? Quantity { get; init; }
	
	/// <summary>
	/// Type ID of the item being acted upon
	/// </summary>
	[JsonPropertyName("quantity_id")]
	public long? TypeId { get; init; }

	
	[MemberNotNullWhen(true, nameof(PasswordType))]
	public bool IsPasswordAction => Action is AlscAction.EnterPassword or AlscAction.SetPassword;
	
}
