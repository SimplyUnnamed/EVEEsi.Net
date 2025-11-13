using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EveEsi.Net.Utilities;

internal class JsonStringEnumMemberConverter : JsonConverterFactory
{
	private static readonly ConcurrentDictionary<Type, Dictionary<string, string>> _enumMappings = new();
	private readonly bool allowIntegerValues;
	private readonly JsonStringEnumConverter baseConverter;

	private readonly JsonNamingPolicy? namingPolicy;

	public JsonStringEnumMemberConverter() : this(null) { }

	public JsonStringEnumMemberConverter(JsonNamingPolicy? namingPolicy = null, bool allowIntegerValues = true)
	{
		this.namingPolicy = namingPolicy;
		this.allowIntegerValues = allowIntegerValues;
		baseConverter = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
	}

	public override bool CanConvert(Type typeToConvert)
	{
		return baseConverter.CanConvert(typeToConvert);
	}

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		Dictionary<string, string> dictionary = _enumMappings.GetOrAdd(typeToConvert, type =>
		{
			return type.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Select(field => (field.Name, attr: field.GetCustomAttribute<EnumMemberAttribute>()?.Value))
				.Where(p => p.attr != null)
				.ToDictionary(p => p.Name, p => p.attr!);
		});

		if (dictionary.Count > 0)
		{
			return new JsonStringEnumConverter(new DictionaryLookupNamingPolicy(dictionary, namingPolicy),
				allowIntegerValues).CreateConverter(typeToConvert, options);
		}

		return baseConverter.CreateConverter(typeToConvert, options);
	}
}

public class JsonNamingPolicyDecorator : JsonNamingPolicy
{
	private readonly JsonNamingPolicy? underlyingNamingPolicy;

	public JsonNamingPolicyDecorator(JsonNamingPolicy? underlyingNamingPolicy)
	{
		this.underlyingNamingPolicy = underlyingNamingPolicy;
	}

	public override string ConvertName(string name)
	{
		return underlyingNamingPolicy?.ConvertName(name) ?? name;
	}
}

internal class DictionaryLookupNamingPolicy : JsonNamingPolicyDecorator
{
	private readonly Dictionary<string, string> dictionary;

	public DictionaryLookupNamingPolicy(Dictionary<string, string> dictionary, JsonNamingPolicy? underlyingNamingPolicy)
		: base(underlyingNamingPolicy)
	{
		this.dictionary = dictionary ?? throw new ArgumentNullException();
	}

	public override string ConvertName(string name)
	{
		return dictionary.TryGetValue(name, out string? value) ? value : base.ConvertName(name);
	}
}
