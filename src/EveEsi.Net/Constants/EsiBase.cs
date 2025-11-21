using System.Runtime.Serialization;
using System.Threading.Tasks.Dataflow;
using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;

namespace EveEsi.Net;

public partial class ESI
{
	internal const string HttpClientName = "EveOnlineEsiClient";
	internal const string EsiBaseUrl = "https://esi.evetech.net";
	internal const string Version = "1.0.0";
	internal const string GithubAddress = "https://github.com/EveEsi/EveEsi";
	internal static readonly DateOnly CompatibilityDate = new(2025, 10, 1);


	public static string[] EsiScopes =
	{
		"publicData", "esi-fleets.write_fleet.v1", "esi-corporations.read_projects.v1",
		"esi-calendar.respond_calendar_events.v1", "esi-calendar.read_calendar_events.v1",
		"esi-location.read_location.v1", "esi-location.read_ship_type.v1", "esi-mail.organize_mail.v1",
		"esi-mail.read_mail.v1", "esi-mail.send_mail.v1", "esi-skills.read_skills.v1",
		"esi-skills.read_skillqueue.v1", "esi-wallet.read_character_wallet.v1",
		"esi-wallet.read_corporation_wallet.v1", "esi-search.search_structures.v1", "esi-clones.read_clones.v1",
		"esi-characters.read_contacts.v1", "esi-universe.read_structures.v1", "esi-killmails.read_killmails.v1",
		"esi-corporations.read_corporation_membership.v1", "esi-assets.read_assets.v1",
		"esi-planets.manage_planets.v1", "esi-fleets.read_fleet.v1", "esi-ui.open_window.v1",
		"esi-ui.write_waypoint.v1", "esi-characters.write_contacts.v1", "esi-fittings.read_fittings.v1",
		"esi-fittings.write_fittings.v1", "esi-markets.structure_markets.v1", "esi-corporations.read_structures.v1",
		"esi-characters.read_loyalty.v1", "esi-characters.read_chat_channels.v1", "esi-characters.read_medals.v1",
		"esi-characters.read_standings.v1", "esi-characters.read_agents_research.v1",
		"esi-industry.read_character_jobs.v1", "esi-markets.read_character_orders.v1",
		"esi-characters.read_blueprints.v1", "esi-characters.read_corporation_roles.v1",
		"esi-location.read_online.v1", "esi-contracts.read_character_contracts.v1", "esi-clones.read_implants.v1",
		"esi-characters.read_fatigue.v1", "esi-killmails.read_corporation_killmails.v1",
		"esi-corporations.track_members.v1", "esi-wallet.read_corporation_wallets.v1",
		"esi-characters.read_notifications.v1", "esi-corporations.read_divisions.v1",
		"esi-corporations.read_contacts.v1", "esi-assets.read_corporation_assets.v1",
		"esi-corporations.read_titles.v1", "esi-corporations.read_blueprints.v1",
		"esi-contracts.read_corporation_contracts.v1", "esi-corporations.read_standings.v1",
		"esi-corporations.read_starbases.v1", "esi-industry.read_corporation_jobs.v1",
		"esi-markets.read_corporation_orders.v1", "esi-corporations.read_container_logs.v1",
		"esi-industry.read_character_mining.v1", "esi-industry.read_corporation_mining.v1",
		"esi-planets.read_customs_offices.v1", "esi-corporations.read_facilities.v1",
		"esi-corporations.read_medals.v1", "esi-characters.read_titles.v1", "esi-alliances.read_contacts.v1",
		"esi-characters.read_fw_stats.v1", "esi-corporations.read_fw_stats.v1"
	};

	public static class Parameters
	{
		public static class Route
		{
			public const string CharacterId = "character_id";
			public const string AllianceId = "alliance_id";
			public const string CorporationId = "corporation_id";
			public const string EventId = "event_id";
			public const string ContractId = "contract_id";
			public const string RegionId = "region_id";
			public const string StarbaseId = "starbase_id";
			public const string AttributeId = "attribute_id";
			public const string EffectId = "effect_id";
			public const string TypeId = "type_id";
			public const string ItemId = "item_id";
			public const string FittingId = "fitting_id";
			public const string FleetId = "fleet_id";
			public const string MemberId = "member_id";
			public const string SquadId = "squad_id";
			public const string WingId = "wing_id";
			public const string KillmailId = "killmail_id";
			public const string KillmainHash = "killmail_hash ";
			public const string MailId = "mail_id";
			public const string LabelId = "label_id";
			public const string MarketGroupId = "market_group_id";
			public const string StructureId = "structure_id";
			public const string PlanetId = "planet_id";
			public const string SchematicId = "schematic_id";
			public const string RouteDestination = "destination";
			public const string RouteOrigin = "origin";
			public const string AsteroidBeltId = "asteroid_belt_id";
			public const string ConstellationId = "constellation_id";
			public const string GraphicId = "graphic_id";
			public const string ItemCategoryId = "category_id";
			public const string ItemGroupId = "group_id";
			public const string MoonId = "moon_id";
			public const string SystemId = "system_id";
			public const string StargateId = "stargate_id";
			public const string StarId = "star_id";
			public const string StationId = "station_id";
			public const string Division = "division";
			public const string WarId = "war_id";
		}

		public static class Query
		{
			public const string Datasource = "datasource";
			public const string Page = "page";
			public const string FromEvent = "from_event";
			public const string Standing = "standing";
			public const string Watched = "watched";
			public const string LableIds = "label_ids";
			public const string ContactIds = "contact_ids";
			public const string SystemId = "system_id";
			public const string IncludeCompleted = "include_completed";
			public const string ObserverId = "observer_id";
			public const string LastMailId = "last_mail_id";
			public const string Labels = "labels";
			public const string RegionTypeId = "type_id";
			public const string RegionOrderType = "order_type";
			public const string RouteFlag = "flag";
			public const string AvoidSolarSystems = "avoid";
			public const string SolarSystemsConnections = "connections";
			public const string Search = "search";
			public const string SearchCategories = "categories";
			public const string SearchStrict = "strict";
			public const string StructuresFilter = "filter";
			public const string ContractId = "contract_id";
			public const string TargetId = "target_id";
			public const string ItemTypeId = "type_id";
			public const string WaypointDestinationId = "destination_id";
			public const string WaypointAddToBeginning = "add_to_beginning";
			public const string WaypointClearOtherWaypoints = "clear_other_waypoints";
			public const string FromId = "from_id";
			public const string MaxWarId = "max_war_id";
		}
	}

	public static class Endpoints
	{
		public enum RateLimitGroup
		{
			[EnumMember(Value = "char-location")] 
			CharLocation,
			[EnumMember(Value = "corp-social")] 
			CorpSocial,
			[EnumMember(Value = "corp-contract")] 
			CorpContract,
			[EnumMember(Value = "corp-industry")] 
			CorpIndustry,
			[EnumMember(Value = "corp-wallet")] 
			CorpWallet,
			[EnumMember(Value = "corp-detail")]
			CorpDetail,
			[EnumMember(Value = "corp-member")]
			CorpMember,
		}

		public static class Characters
		{
			public const string PublicInformation = "get_characters_character_id";
			public const string Standings = "get_characters_character_id_standings";
			public const string AgentsResearch = "get_characters_character_id_agents_research";
			public const string Blueprints = "get_characters_character_id_blueprints";
			public const string CorporationHistory = "get_characters_character_id_corporationhistory";
			public const string CSPA = "post_characters_character_id_cspa";
			public const string Fatigue = "get_characters_character_id_fatigue";
			public const string Medals = "get_characters_character_id_medals";
			public const string Notifications = "get_characters_character_id_notifications";
			public const string ContactNotifications = "get_characters_character_id_notifications_contacts";
			public const string Portrait = "get_characters_character_id_portrait";
			public const string Roles = "get_characters_character_id_roles";
			public const string Titles = "get_characters_character_id_titles";
			public const string Affilation = "post_characters_affiliation";
		}

		public static class Alliances
		{
			public const string ActiveAlliances = "get_alliance";
			public const string PublicInformation = "get_alliances_alliance_id";
			public const string CorporationsInAlliance = "get_alliances_alliance_id_corporations";
			public const string AllianceIcon = "get_alliances_alliance_id_icons";
		}

		public static class Assets
		{
			public const string CharacterAssetList = "get_characters_character_id_assets";
			public const string CharacterAssetLocations = "post_characters_character_id_assets_locations";
			public const string CharacterAssetNames = "post_characters_character_id_assets_names";
			public const string CorporationAssetList = "get_corporations_corporation_id_assets";
			public const string CorporationAssetLocations = "post_corporations_corporation_id_assets_locations";
			public const string CorporationAssetNames = "post_corporations_corporation_id_assets_names";
		}

		public static class Calendar
		{
			public const string CalendarItems = "get_characters_character_id_calendar";
			public const string CalendarEvent = "get_characters_character_id_calendar_event_id";
			public const string RespondToEvent = "put_characters_character_id_calendar_event_id";
			public const string EventAttendees = "get_characters_character_id_calendar_event_id_attendees";
		}

		public static class Clones
		{
			public const string CloneList = "get_characters_character_id_clones";
			public const string CloneImplants = "get_characters_character_id_implants";
		}

		public static class Contacts
		{
			public const string AllianceContacts = "get_alliances_alliance_id_contacts";
			public const string AllianceContactLabels = "get_alliances_alliance_id_contacts_labels";
			public const string DeleteCharacterContacts = "delete_characters_character_id_contacts";
			public const string CharacterContacts = "get_characters_character_id_contacts";
			public const string AddCharacterContacts = "post_characters_character_id_contacts";
			public const string UpdateCharacterContacts = "put_characters_character_id_contacts";
			public const string CharacterContactLabels = "get_characters_character_id_contacts_labels";
			public const string CorporationContacts = "get_corporations_corporation_id_contacts";
			public const string CorporationContactLabels = "get_corporations_corporation_id_contacts_labels";
		}

		public static class Contracts
		{
			public const string CharacterContracts = "get_characters_character_id_contracts";
			public const string CharacterContractBids = "get_characters_character_id_contracts_contract_id_bids";
			public const string CharacterContractItems = "get_characters_character_id_contracts_contract_id_items";
			public const string PublicContracts = "get_contracts_public_region_id";
			public const string PublicContractBids = "get_contracts_public_bids_contract_id";
			public const string PublicContractItems = "get_contracts_public_items_contract_id";
			public const string CorporationContracts = "get_corporations_corporation_id_contracts";
			public const string CorporationContractBids = "get_corporations_corporation_id_contracts_contract_id_bids";

			public const string CorporationContractItems =
				"get_corporations_corporation_id_contracts_contract_id_items";
		}

		public static class Corporation
		{
			public const string Information = "get_corporations_corporation_id";
			public const string AllianceHistory = "get_corporations_corporation_id_alliancehistory";
			public const string Blueprints = "get_corporations_corporation_id_blueprints";
			public const string ContainersLogs = "get_corporations_corporation_id_containers_logs";
			public const string Divisions = "get_corporations_corporation_id_divisions";
			public const string Facilities = "get_corporations_corporation_id_facilities";
			public const string Icons = "get_corporations_corporation_id_icons";
			public const string Medals = "get_corporations_corporation_id_medals";
			public const string IssuedMedals = "get_corporations_corporation_id_medals_issued";
			public const string Members = "get_corporations_corporation_id_members";
			public const string MembersLimit = "get_corporations_corporation_id_members_limit";
			public const string MembersTitles = "get_corporations_corporation_id_members_titles";
			public const string MemberTracking = "get_corporations_corporation_id_membertracking";
			public const string Roles = "get_corporations_corporation_id_roles";
			public const string RolesHistory = "get_corporations_corporation_id_roles_history";
			public const string Shareholders = "get_corporations_corporation_id_shareholders";
			public const string Standings = "get_corporations_corporation_id_standings";
			public const string Starbases = "get_corporations_corporation_id_starbases";
			public const string StarbaseInfo = "get_corporations_corporation_id_starbases_starbase";
			public const string Structures = "get_corporations_corporation_id_structures";
			public const string Titles = "get_corporations_corporation_id_titles";
			public const string NpcCorporations = "get_corporations_npccorps";
		}

		public static class Dogma
		{
			public const string Attributes = "get_dogma_attributes";
			public const string AttributeInfo = "get_dogma_attributes_attribute_id";
			public const string DynamicItemInfo = "get_dogma_dynamic_items_type_id_item_id";
			public const string Effects = "get_dogma_effects";
			public const string EffectInfo = "get_dogma_effects_effect_id";
		}

		public static class FactionWarfare
		{
			public const string CharacterStats = "get_characters_character_id_fw_stats";
			public const string CorporationStats = "get_corporations_corporation_id_fw_stats";
			public const string FactionsLeaderboard = "get_fw_leaderboards";
			public const string CaractersLeaderboard = "get_fw_leaderboards_characters";
			public const string CorporationsLeaderboard = "get_fw_leaderboards_corporations";
			public const string FactionsStats = "get_fw_stats";
			public const string OwnershipSystemOverview = "get_fw_systems";
			public const string Wars = "get_fw_wars";
		}

		public static class Fittings
		{
			public const string GetFittings = "get_characters_character_id_fittings";
			public const string DeleteFitting = "delete_characters_character_id_fitting_id_fittings";
			public const string NewFitting = "post_characters_character_id_fittings";
		}

		public static class Fleets
		{
			public const string FleetInfo = "get_characters_character_id_fleet";
			public const string FleetSettings = "get_fleets_fleet_id";
			public const string UpdateFleetSettings = "put_fleets_fleet_id";
			public const string FleetMembers = "get_fleets_fleet_id_members";
			public const string InviteMember = "post_fleets_fleet_id_members";
			public const string KickMember = "delete_fleets_fleet_id_members_member_id";
			public const string MoveMember = "put_fleets_fleet_id_members_member_id";
			public const string DeleteSquad = "delete_fleets_fleet_id_squads_squad_id";
			public const string RenameSquad = "put_fleets_fleet_id_squads_squad_id";
			public const string FleetWings = "get_fleets_fleet_id_wings";
			public const string NewWing = "post_fleets_fleet_id_wings";
			public const string DeleteWing = "delete_fleets_fleet_id_wings_wing_id";
			public const string RenameWing = "put_fleets_fleet_id_wings_wing_id";
			public const string NewSquad = "post_fleets_fleet_id_wings_wing_id_squads";
		}

		public static class Incursions
		{
			public const string IncursionList = "get_incursions";
		}

		public static class Industry
		{
			public const string CharacterJobs = "get_characters_character_id_industry_jobs";
			public const string CharacterMiningLedger = "get_characters_character_id_mining";
			public const string ExtractionTimers = "get_corporation_corporation_id_mining_extractions";
			public const string CorporationObservers = "get_corporation_corporation_id_mining_observers";
			public const string ObserverInfo = "get_corporation_corporation_id_mining_observers_observer_id";
			public const string CorporationJobs = "get_corporations_corporation_id_industry_jobs";
			public const string Facilities = "get_industry_facilities";
			public const string SolarSystems = "get_industry_systems";
		}

		public static class Insurence
		{
			public const string InsuranceLevels = "get_insurance_prices";
		}

		public static class Killmails
		{
			public const string CharacterKillmails = "get_characters_character_id_killmails_recent";
			public const string CorporationKillmails = "get_corporations_corporation_id_killmails_recent";
			public const string KillmailInfo = "get_killmails_killmail_id_killmail_hash";
		}

		public static class Location
		{
			public const string CurrentLocation = "get_characters_character_id_location";
			public const string Online = "get_characters_character_id_online";
			public const string CurrentShip = "get_characters_character_id_ship";
		}

		public static class Loyalty
		{
			public const string LoyaltyPoints = "get_characters_character_id_loyalty_points";
			public const string CorporationOffers = "get_loyalty_stores_corporation_id_offers";
		}

		public static class Mail
		{
			public const string MailHeaders = "get_characters_character_id_mail";
			public const string SendMail = "post_characters_character_id_mail";
			public const string DeleteMail = "delete_characters_character_id_mail_mail_id";
			public const string GetMail = "get_characters_character_id_mail_mail_id";
			public const string UpdateMail = "put_characters_character_id_mail_mail_id";
			public const string GetLabels = "get_characters_character_id_mail_labels";
			public const string CreateLabel = "post_characters_character_id_mail_labels";
			public const string DeleteLabel = "delete_characters_character_id_mail_labels_label_id";
			public const string MailingList = "get_characters_character_id_mail_lists";
		}

		public static class Market
		{
			public const string CharacterOrders = "get_characters_character_id_orders";
			public const string CharacterOrdersHistory = "get_characters_character_id_orders_history";
			public const string CorporationOrders = "get_corporations_corporation_id_orders";
			public const string CorporationOrdersHistory = "get_corporations_corporation_id_orders_history";
			public const string RegionStatistics = "get_markets_region_id_history";
			public const string RegionOrders = "get_markets_region_id_orders";
			public const string ActiveRegionOrderTypes = "get_markets_region_id_types";
			public const string MarketGroups = "get_markets_groups";
			public const string MarketGroupInfo = "get_markets_groups_market_group_id";
			public const string TypePrices = "get_markets_prices";
			public const string StructureOrders = "get_markets_structures_structure_id";
		}

		public static class PlanetaryInteraction
		{
			public const string Colonies = "get_characters_character_id_planets";
			public const string ColonyInfo = "get_characters_character_id_planets_planet_id";
			public const string CustomOffices = "get_corporations_corporation_id_customs_offices";
			public const string SchematicInfo = "get_universe_schematics_schematic_id";
		}

		public static class Routes
		{
			public const string Route = "get_route_origin_destination";
		}

		public static class Search
		{
			public const string Query = "get_characters_character_id_search";
		}

		public static class Skills
		{
			public const string Attributes = "get_characters_character_id_attributes";
			public const string SkillQueue = "get_characters_character_id_skillqueue";
			public const string SkillDetails = "get_characters_character_id_skills";
		}

		public static class Sovereignty
		{
			public const string Campaigns = "get_sovereignty_campaigns";
			public const string SolarSystems = "get_sovereignty_map";
			public const string Structures = "get_sovereignty_structures";
		}

		public static class Status
		{
			public const string ServerStatus = "get_status";
		}

		public static class Universe
		{
			public const string Ancestries = "get_universe_ancestries";
			public const string AsteroidBeltInfo = "get_universe_asteroid_belts_asteroid_belt_id";
			public const string Bloodlines = "get_universe_bloodlines";
			public const string ItemCategories = "get_universe_categories";
			public const string ItemCategoryInfo = "get_universe_categories_category_id";
			public const string Constellations = "get_universe_constellations";
			public const string ConstellationInfo = "get_universe_constellations_constellation_id";
			public const string Factions = "get_universe_factions";
			public const string Graphics = "get_universe_graphics";
			public const string GraphicInfo = "get_universe_graphics_graphic_id";
			public const string ItemGroups = "get_universe_groups";
			public const string ItemGroupInfo = "get_universe_groups_group_id";
			public const string IDs = "post_universe_ids";
			public const string MoonInfo = "get_universe_moons_moon_id";
			public const string Names = "post_universe_names";
			public const string PlanetInfo = "get_universe_planets_planet_id";
			public const string Races = "get_universe_races";
			public const string Regions = "get_universe_regions";
			public const string RegionInfo = "get_universe_regions_region_id";
			public const string StargateInfo = "get_universe_stargates_stargate_id";
			public const string StarInfo = "get_universe_stars_star_id";
			public const string StationInfo = "get_universe_stations_station_id";
			public const string Structures = "get_universe_structures";
			public const string StructureInfo = "get_universe_structures_structure_id";
			public const string SystemJumps = "get_universe_system_jumps";
			public const string SystemKills = "get_universe_system_kills";
			public const string SolarSystems = "get_universe_systems";
			public const string SolarSystemInfo = "get_universe_systems_system_id";
			public const string Types = "get_universe_types";
			public const string TypeInfo = "get_universe_types_type_id";
		}

		public static class UserInterface
		{
			public const string SetAutopilotWaypoint = "post_ui_autopilot_waypoint";
			public const string OpenContractWindow = "post_ui_openwindow_contractwindow";
			public const string OpenInformationWindow = "post_ui_openwindow_ingormationwindow";
			public const string OpenMarketDetails = "post_ui_openwindow_marketdetails";
			public const string OpenNewMailWindow = "post_ui_openwindow_newmail";
		}

		public static class Wallet
		{
			public const string WalletBalance = "get_characters_character_id_wallet";
			public const string WalletJournal = "get_characters_character_id_wallet_journal";
			public const string WalletTransactions = "get_characters_character_id_wallet_transactions";
			public const string CorporationWallets = "get_corporations_corporation_id_wallets";
			public const string CorporationWalletJournal = "get_corporations_corporation_id_wallets_division_journal";

			public const string CorporationWalletTransactions =
				"get_corporations_corporation_id_wallets_division_transactions";
		}

		public static class Wars
		{
			public const string WarList = "get_wars";
			public const string WarDetails = "get_wars_war_id";
			public const string Kills = "get_wars_war_id_killmails";
		}
	}
}
