using System.Collections;

namespace EveEsi.Net.EsiClient;

public class RequestParameters : IEnumerable<KeyValuePair<string, string>>
{
	private readonly List<KeyValuePair<string, string>> _queryParameters = new();
	private readonly List<KeyValuePair<string, string>> _routeParameters = new();

	private object? _body;

	public RequestParameters()
	{
		Route = new RequestParameterCollection(_routeParameters);
		Query = new RequestParameterCollection(_queryParameters);
	}

	public RequestParameterCollection Route { get; }

	public RequestParameterCollection Query { get; }

	public object? Body
	{
		get => _body;
		set => _body = value ?? throw new ArgumentNullException(nameof(Body));
	}

	public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
	{
		foreach (KeyValuePair<string, string> parameter in _routeParameters)
		{
			yield return parameter;
		}

		foreach (KeyValuePair<string, string> parameter in _queryParameters)
		{
			yield return parameter;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

public class RequestParameterCollection : IEnumerable<KeyValuePair<string, string>>
{
	private readonly List<KeyValuePair<string, string>> _parameters;

	public RequestParameterCollection(List<KeyValuePair<string, string>> parameters)
	{
		_parameters = parameters;
	}

	public string? this[string key]
	{
		get => GetParameter(key);
		set => SetParameter(key, value);
	}

	public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
	{
		return _parameters.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public bool ContainsKey(string key)
	{
		ArgumentException.ThrowIfNullOrEmpty(key);

		return this[key] != null;
	}

	protected virtual void SetParameter(string key, string? value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return;
		}

		int index = _parameters.FindIndex(p => p.Key == key);
		if (index >= 0)
		{
			_parameters[index] = new KeyValuePair<string, string>(key, value);
		}
		else
		{
			_parameters.Add(new KeyValuePair<string, string>(key, value));
		}
	}

	protected virtual string? GetParameter(string key)
	{
		KeyValuePair<string, string> parameter = _parameters.Find(p => p.Key == key);
		if (parameter.Equals(default(KeyValuePair<string, string>)))
		{
			return null;
		}

		return parameter.Value;
	}
}
