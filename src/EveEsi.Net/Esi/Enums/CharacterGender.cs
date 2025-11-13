using System.Text.Json.Serialization;
using EveEsi.Net.Utilities;

namespace EveEsi.Net.Esi.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CharacterGender
{
	Male,
	Female
}
