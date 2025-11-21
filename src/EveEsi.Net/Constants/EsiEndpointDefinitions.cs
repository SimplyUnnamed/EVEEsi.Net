using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;

namespace EveEsi.Net;

public partial class ESI
{
	internal static IReadOnlyDictionary<string, Action<EsiEndpointBuilder>> EsiEndpointDefinitions =>
		new Dictionary<string, Action<EsiEndpointBuilder>>
		{
			// Alliance Endpoints
			#region Alliance Endpoints

			{
				Endpoints.Alliances.PublicInformation, b =>
				{
					b.Route = $"/alliances/[{Parameters.Route.AllianceId}]";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Alliances.CorporationsInAlliance, b =>
				{
					b.Route = $"/alliances/[{Parameters.Route.AllianceId}]/corporations";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Alliances.ActiveAlliances, b =>
				{
					b.Route = $"/alliances/[{Parameters.Route.AllianceId}]/corporations";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Alliances.AllianceIcon, b =>
				{
					b.Route = $"/alliances/[{Parameters.Route.AllianceId}]/icons";
					b.CacheExpiry = new DailyExpiry(new TimeOnly(11, 5));
				}
			},

			#endregion

			// Assets Endpoints

			#region Asset Endpoints

			{
				Endpoints.Assets.CharacterAssetList, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/assets";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-assets.read_assets.v1");
				}
			},
			{
				Endpoints.Assets.CharacterAssetNames, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/assets/names";
					b.HttpMethodType = HttpMethodType.Post;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-assets.read_assets.v1");
				}
			},
			{
				Endpoints.Assets.CharacterAssetLocations, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/assets/locations";
					b.HttpMethodType = HttpMethodType.Post;
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-assets.read_assets.v1");
				}
			},
			{
				Endpoints.Assets.CorporationAssetList, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/assets";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-assets.read_corporation_assets.v1");
				}
			},
			{
				Endpoints.Assets.CorporationAssetNames, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/assets/names";
					b.HttpMethodType = HttpMethodType.Post;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-assets.read_corporation_assets.v1");
				}
			},
			{
				Endpoints.Assets.CorporationAssetLocations, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/assets/locations";
					b.HttpMethodType = HttpMethodType.Post;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-assets.read_corporation_assets.v1");
				}
			},

			#endregion

			// Calendar Endpoints

			#region Calendar Endpoints

			{
				Endpoints.Calendar.CalendarItems, b =>
				{
					b.Route = $"/calendars/[{Parameters.Route.CharacterId}]/calendar";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-calendar.read_calendar.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromSeconds(5));
				}
			},
			{
				Endpoints.Calendar.CalendarEvent, b =>
				{
					b.Route = $"/calendars/[{Parameters.Route.CharacterId}]/calendar/[{Parameters.Route.EventId}]";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-calendar.read_calendar.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromSeconds(5));
				}
			},
			{
				Endpoints.Calendar.RespondToEvent, b =>
				{
					b.Route = $"/calendars/[{Parameters.Route.CharacterId}]/calendar/[{Parameters.Route.EventId}]";
					b.HttpMethodType = HttpMethodType.Put;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-calendar.respond_calendar_events.v1");
				}
			},
			{
				Endpoints.Calendar.EventAttendees, b =>
				{
					b.Route =
						$"/calendars/[{Parameters.Route.CharacterId}]/calendar/[{Parameters.Route.EventId}]/attendees";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-calendar.read_calendar.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(10));
				}
			},

			#endregion

			// Character Endpoints

			#region Character Endpoints

			{
				Endpoints.Characters.Affilation, b =>
				{
					b.Route = "/characters/affiliation";
					b.HttpMethodType = HttpMethodType.Post;
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Characters.PublicInformation, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromDays(30));
				}
			},
			{
				Endpoints.Characters.AgentsResearch, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/agents_research";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_agents_research.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Characters.Blueprints, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/blueprints";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_blueprints.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Characters.CorporationHistory, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/corporationhistory";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromDays(1));
				}
			},
			{
				Endpoints.Characters.CSPA, b =>
				{
					b.HttpMethodType = HttpMethodType.Post;
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/cspa";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_contacts.v1");
				}
			},
			{
				Endpoints.Characters.Fatigue, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/fatigue";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_fatigue.v1");
					b.CacheExpiry =  new TimeBasedExpiry(TimeSpan.FromMinutes(5));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CharLocation;
				}
			},
			{
				Endpoints.Characters.Medals, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/medals";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_medals.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Characters.Notifications, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/notifications";
					b.AuthenticatedEndpoint =  new AuthenticatedEndpoint("esi-characters.read_notifications.v1");
					b.CacheExpiry =  new TimeBasedExpiry(TimeSpan.FromMinutes(10));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CharLocation;
				}
			},
			{
				Endpoints.Characters.ContactNotifications, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/notifications/contacts";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_notifications.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(10));
				}
			},
			{
				Endpoints.Characters.Portrait, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/portrait";
					b.CacheExpiry = new DailyExpiry(new TimeOnly(11, 05));
				}
			},
			{
				Endpoints.Characters.Roles, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/roles";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_corporation_roles.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Characters.Standings, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/standings";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_standings.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Characters.Titles, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/titles";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_titles.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},

			#endregion

			// Clone Endpoints

			#region Clone Endpoints

			{
				Endpoints.Clones.CloneList, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/clones";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_clones.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(2));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CharLocation;
				}
			},
			{
				Endpoints.Clones.CloneImplants, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/implants";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_implants.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(2));
				}
			},

			#endregion

			// Contacts Endpoints

			#region Contact Endpoints

			{
				Endpoints.Contacts.AllianceContacts, b =>
				{
					b.Route = $"/alliances/[{Parameters.Route.AllianceId}]/contacts";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-alliances.read_contacts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contacts.AllianceContactLabels, b =>
				{
					b.Route = $"/alliances/[{Parameters.Route.AllianceId}]/contacts/labels";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-alliances.read_contacts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contacts.DeleteCharacterContacts, b =>
				{
					b.HttpMethodType = HttpMethodType.Delete;
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/contacts";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.write_contacts.v1");
				}
			},
			{
				Endpoints.Contacts.CharacterContacts, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/contacts";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_contacts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contacts.AddCharacterContacts, b =>
				{
					b.HttpMethodType = HttpMethodType.Post;
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/contacts";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.write_contacts.v1");
				}
			},
			{
				Endpoints.Contacts.CharacterContactLabels, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/contacts/labels";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-characters.read_contacts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contacts.CorporationContacts, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/contacts";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_contacts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpSocial;
				}
			},
			{
				Endpoints.Contacts.CorporationContactLabels, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/contacts/labels";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_contacts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpSocial;
				}
			},

			#endregion

			// Contract Endpoints

			#region Contract Endpoints

			{
				Endpoints.Contracts.CharacterContracts, b =>
				{
					b.Route = $"/characters/[{Parameters.Route.CharacterId}]/contracts";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-contracts.read_character_contracts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contracts.CharacterContractBids, b =>
				{
					b.Route =
						$"/characters/[{Parameters.Route.CharacterId}]/contracts/[{Parameters.Route.ContractId}]/bids";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-contracts.read_character_contracts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contracts.CharacterContractItems, b =>
				{
					b.Route =
						$"/characters/[{Parameters.Route.CharacterId}]/contracts/[{Parameters.Route.ContractId}]/items";
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-contracts.read_character_contracts.v1");
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Contracts.PublicContracts, b =>
				{
					b.Route = $"/contracts/public/[{Parameters.Route.RegionId}]";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(30));
				}
			},
			{
				Endpoints.Contracts.PublicContractBids, b =>
				{
					b.Route = $"/contracts/public/bids/[{Parameters.Route.ContractId}]";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contracts.PublicContractItems, b =>
				{
					b.Route = $"/contracts/public/items/[{Parameters.Route.ContractId}]";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
				}
			},
			{
				Endpoints.Contracts.CorporationContracts, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/contracts";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpContract;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-contracts.read_corporation_contracts.v1");
				}
			},
			{
				Endpoints.Contracts.CorporationContractBids, b =>
				{
					b.Route =
						$"/corporations/[{Parameters.Route.CorporationId}]/contracts/[{Parameters.Route.ContractId}]/bids";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(5));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpContract;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-contracts.read_corporation_contracts.v1");
				}
			},
			{
				Endpoints.Contracts.CorporationContractItems, b =>
				{
					b.Route =
						$"/corporations/[{Parameters.Route.CorporationId}]/contracts/[{Parameters.Route.ContractId}]/items";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpContract;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-contracts.read_corporation_contracts.v1");
				}
			},

			#endregion

			// Corporation Endpoints

			#region Corporation Endpoints

			{
				Endpoints.Corporation.NpcCorporations, b =>
				{
					b.Route = "/corporations/npccorps";
					b.CacheExpiry = new DailyExpiry(new TimeOnly(11, 05));
				}
			},
			{
				Endpoints.Corporation.Information, b =>
				{
					b.Route = $"'/corporations/[{Parameters.Route.CorporationId}]";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Corporation.AllianceHistory, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/aliancehistory";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Corporation.Blueprints, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/blueprints";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpIndustry;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_blueprints.v1");
				}
			},
			{
				Endpoints.Corporation.ContainersLogs, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/container/logs";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromMinutes(10));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_container_logs.v1");
				}
			},
			{
				Endpoints.Corporation.Divisions, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/divisions";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpWallet;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_divisions.v1");
				}
			},
			{
				Endpoints.Corporation.Facilities, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/facilities";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_facilities.v1");
				}
			},
			{
				Endpoints.Corporation.Icons, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/icons";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
				}
			},
			{
				Endpoints.Corporation.Medals, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/medals";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpDetail;
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_medals.v1");
				}
			},
			{
				Endpoints.Corporation.IssuedMedals, b =>
				{
					b.Route = $"/corporations/[{Parameters.Route.CorporationId}]/medals/issued";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_medals.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpDetail;
				}
			},
			{
				Endpoints.Corporation.Members, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/members";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_corporation_membership.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpMember;
				}
			},
			{
				Endpoints.Corporation.MembersLimit, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/members/limit";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.track_members.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpMember;
				}
			},
			{
				Endpoints.Corporation.MembersTitles, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/members/titles";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_titles.v1.");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpMember;
				}
			},
			{
				Endpoints.Corporation.MemberTracking, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/memberstracking";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.track_members.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpMember;
				}
			},
			{
				Endpoints.Corporation.Roles, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/roles";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_corporation_membership.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpMember;
				}
			},
			{
				Endpoints.Corporation.RolesHistory, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/roles/history";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_corporation_membership.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpMember;
				}
			},
			{
				Endpoints.Corporation.Shareholders, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/shareholders";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-wallet.read_corporation_wallets.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpDetail;
				}
			},
			{
				Endpoints.Corporation.Standings, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/standings";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_standings.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpMember;
				}
			},
			{
				Endpoints.Corporation.Starbases, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/starbases";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_starbases.v1");
				}
			},
			{
				Endpoints.Corporation.StarbaseInfo, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/starbases/[{Parameters.Route.StarbaseId}]";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_starbases.v1");
				}
			},
			{
				Endpoints.Corporation.Structures, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/structures";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_structures.v1");
				}
			},
			{
				Endpoints.Corporation.Titles, b =>
				{
					b.Route = $"/corporations/{Parameters.Route.CorporationId}/titles";
					b.CacheExpiry = new TimeBasedExpiry(TimeSpan.FromHours(1));
					b.AuthenticatedEndpoint = new AuthenticatedEndpoint("esi-corporations.read_titles.v1");
					b.RateLimitGroup = Endpoints.RateLimitGroup.CorpDetail;
				}
			}
			#endregion
		};
}
