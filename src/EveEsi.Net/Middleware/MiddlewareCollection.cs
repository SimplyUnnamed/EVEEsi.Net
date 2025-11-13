using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Middleware;

internal sealed class MiddlewareCollection : IMiddlewareCollection, IEsiGlobalMiddlewareCollection
{
	private readonly List<MiddlewareComponent> _middleware;

	/// <summary>
	///     Create an empty collection
	/// </summary>
	public MiddlewareCollection()
	{
		_middleware = new List<MiddlewareComponent>();
	}

	/// <summary>
	///     Create a new collection based on an exiting collection.
	/// </summary>
	/// <param name="middlewares">The exisitng list of middlewares</param>
	private MiddlewareCollection(List<MiddlewareComponent> middlewares)
	{
		_middleware = new List<MiddlewareComponent>(middlewares);
	}

	private bool Frozen { get; set; }

	/// <inheritdoc />
	public IMiddlewareCollection Clone()
	{
		return new MiddlewareCollection(_middleware);
	}

	/// <inheritdoc />
	public IReadOnlyList<MiddlewareComponent> Middleware => _middleware.AsReadOnly();

	/// <inheritdoc />
	public IMiddlewareCollection AddMiddleware(MiddlewareComponent middleware)
	{
		if (Frozen)
		{
			throw new InvalidOperationException("Pipeline is frozen");
		}

		ArgumentNullException.ThrowIfNull(middleware);
		_middleware.Add(middleware);
		return this;
	}

	/// <inheritdoc />
	public EsiRequestDelegate Build(EsiRequestDelegate terminal)
	{
		EsiRequestDelegate next = terminal;
		foreach (MiddlewareComponent md in Middleware.Reverse().OrderBy(m => m.Priority))
		{
			next = md.Middleware(next);
		}

		return next;
	}

	public void Freeze()
	{
		Frozen = true;
	}
}
