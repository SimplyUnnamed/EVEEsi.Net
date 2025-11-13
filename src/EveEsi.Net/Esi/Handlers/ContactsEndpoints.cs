using System.Globalization;
using EveEsi.Net.Esi.Models;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Factories;
using Endpoints = EveEsi.Net.ESI.Endpoints.Contacts;

namespace EveEsi.Net.Handlers;

internal class ContactsEndpoints(IEsiRequestClientFactory clientFactory) : IContactEndpoints
{
	/// <inheritdoc />
	public IRequestClient<AllianceContact[]> GetAllianceContacts(long allianceId, string token, int? page = null)
	{
		return clientFactory.CreateClient<AllianceContact[]>(Endpoints.AllianceContacts, p =>
		{
			p.Route[ESI.Parameters.Route.AllianceId] = allianceId.ToString();
			p.Query[ESI.Parameters.Query.Page] = page?.ToString() ?? "1";
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<ContactLabel[]> GetAllianceContactLabels(long allianceId, string token)
	{
		return clientFactory.CreateClient<ContactLabel[]>(Endpoints.AllianceContactLabels, p =>
		{
			p.Route[ESI.Parameters.Route.AllianceId] = allianceId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient DeleteContacts(long characterId, IEnumerable<long> contactIds, string token)
	{
		long[] enumerable = contactIds as long[] ?? contactIds.ToArray();
		if (enumerable.ListWithinSize(20))
		{
			throw new ArgumentOutOfRangeException(nameof(contactIds), "There must be between 1 and 20 contacts ids");
		}

		return clientFactory.CreateClient(Endpoints.DeleteCharacterContacts, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Query[ESI.Parameters.Query.ContactIds] = string.Join(',', enumerable);
		}, token);
	}

	public IRequestClient<CharacterContact[]> GetCharacterContacts(long characterId, string token, int? page = null)
	{
		return clientFactory.CreateClient<CharacterContact[]>(Endpoints.CharacterContacts, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			p.Query[ESI.Parameters.Query.Page] = page?.ToString() ?? "1";
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<long[]> AddCharacterContact(long characterId, string token, IEnumerable<long> contactIds,
		double standing,
		IEnumerable<long>? labels = null, bool? watched = null)
	{
		long[] characterArray = contactIds as long[] ?? contactIds.ToArray();
		long[]? labelsArray = labels as long[] ?? labels?.ToArray();

		if (!characterArray.ListWithinSize(100))
		{
			throw new ArgumentOutOfRangeException(nameof(contactIds), "There must be between 1 and 100 contacts ids");
		}

		if (labelsArray.ListWithinSize(63, 0))
		{
			throw new ArgumentOutOfRangeException(nameof(labels), "There must be between 1 and 100 contacts ids");
		}

		return clientFactory.CreateClient<long[]>(Endpoints.AddCharacterContacts, b =>
		{
			b.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			b.Query[ESI.Parameters.Query.Standing] = standing.ToString(CultureInfo.InvariantCulture);
			b.Body = characterArray;

			if (labelsArray != null)
			{
				b.Query[ESI.Parameters.Query.Labels] = string.Join(',', labelsArray);
			}

			if (watched != null)
			{
				b.Query[ESI.Parameters.Query.Watched] = watched.Value.ToString(CultureInfo.InvariantCulture);
			}
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient EditCharacterContacts(long characterId, string token, IEnumerable<long> contactIds,
		double standing,
		IEnumerable<long>? labels = null, bool? watched = null)
	{
		long[] characterArray = contactIds as long[] ?? contactIds.ToArray();
		long[]? labelsArray = labels as long[] ?? labels?.ToArray();

		if (!characterArray.ListWithinSize(100))
		{
			throw new ArgumentOutOfRangeException(nameof(contactIds), "There must be between 1 and 100 contacts ids");
		}

		if (labelsArray.ListWithinSize(63, 0))
		{
			throw new ArgumentOutOfRangeException(nameof(labels), "There must be between 1 and 100 contacts ids");
		}

		return clientFactory.CreateClient(Endpoints.UpdateCharacterContacts, b =>
		{
			b.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
			b.Query[ESI.Parameters.Query.Standing] = standing.ToString(CultureInfo.InvariantCulture);
			b.Body = characterArray;

			if (labelsArray != null)
			{
				b.Query[ESI.Parameters.Query.Labels] = string.Join(',', labelsArray);
			}

			if (watched != null)
			{
				b.Query[ESI.Parameters.Query.Watched] = watched.Value.ToString(CultureInfo.InvariantCulture);
			}
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<ContactLabel[]> GetCharacterContactLabels(long characterId, string token)
	{
		return clientFactory.CreateClient<ContactLabel[]>(Endpoints.CharacterContactLabels, p =>
		{
			p.Route[ESI.Parameters.Route.CharacterId] = characterId.ToString();
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<CorporationContact[]> GetCorporationContact(long corporationId, string token,
		int? page = null)
	{
		return clientFactory.CreateClient<CorporationContact[]>(Endpoints.CorporationContacts, p =>
		{
			p.Route[ESI.Parameters.Route.AllianceId] = corporationId.ToString();
			p.Query[ESI.Parameters.Query.Page] = page?.ToString() ?? "1";
		}, token);
	}

	/// <inheritdoc />
	public IRequestClient<ContactLabel[]> GetCorporationContactLabels(long corporationId, string token)
	{
		return clientFactory.CreateClient<ContactLabel[]>(Endpoints.CorporationContactLabels, p =>
		{
			p.Route[ESI.Parameters.Route.CorporationId] = corporationId.ToString();
		}, token);
	}
}
