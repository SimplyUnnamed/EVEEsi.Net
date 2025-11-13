using EveEsi.Net.EsiClient;
using EveEsi.Net.Factories;
using EveEsi.Net.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace EveEsi.Net.Extensions;

internal static class UseEsiMiddlewareExtensions
{
	/// <summary>
	///     Adds a middleware callback function to the pipeline
	/// </summary>
	/// <param name="collection">The Middleware Collection</param>
	/// <param name="middleware">The middleware Callback function</param>
	public static void AddMiddleware(this IMiddlewareCollection collection,
		Func<EsiRequestContext, EsiRequestDelegate, Task> middleware, int priority = 5)
	{
		if (priority is < 1 or > 10)
		{
			throw new ArgumentOutOfRangeException(nameof(priority), "Middleware priority must be between 1 and 10");
		}

		MiddlewareFunctionBinder functionBinder = new(middleware);
		MiddlewareComponent middlewareComponent = new(functionBinder.CreateMiddleware, priority);
		collection.AddMiddleware(middlewareComponent);
	}

	/// <summary>
	///     Adds a Middleware by class to be added to the pipeline.
	/// </summary>
	/// <param name="collection">The Middleware Collection</param>
	/// <param name="priority">The middleware Execution Priority</param>
	/// <typeparam name="TMiddleware">The middleware class</typeparam>
	public static void AddMiddleware<TMiddleware>(this IMiddlewareCollection collection, int priority = 5)
	{
		collection.AddMiddleware(typeof(TMiddleware), priority);
	}

	/// <summary>
	///     Creates a Middleware interface binder and adds created middleware callback into the middleware collection.
	/// </summary>
	/// <param name="collection">The Middleware Collection</param>
	/// <param name="middleware">The Middleware class type</param>
	/// <param name="priority">The middleware Execution Priority</param>
	/// <exception cref="InvalidOperationException">Throws there are errors instantiating the middleware.</exception>
	public static void AddMiddleware(this IMiddlewareCollection collection, Type middleware, int priority = 5)
	{
		if (priority < 1 || priority > 10)
		{
			throw new ArgumentOutOfRangeException(nameof(priority), "Middleware priority must be between 1 and 10");
		}

		if (!typeof(IEsiMiddleware).IsAssignableFrom(middleware))
		{
			throw new InvalidOperationException(
				$"Middleware of type {middleware.Name} does not implement {nameof(IEsiMiddleware)}");
		}

		MiddlewareInterfaceBinder middlewareInterfaceBinder = new(middleware);
		MiddlewareComponent middlewareComponent = new(middlewareInterfaceBinder.CreateMiddleware, priority);
		collection.AddMiddleware(middlewareComponent);
	}

	/// <summary>
	///     Creates a Pipeline Wrapper to call the middleware
	/// </summary>
	private sealed class MiddlewareFunctionBinder
	{
		private readonly Func<EsiRequestContext, EsiRequestDelegate, Task> _middleware;

		public MiddlewareFunctionBinder(Func<EsiRequestContext, EsiRequestDelegate, Task> middleware)
		{
			_middleware = middleware;
		}

		public EsiRequestDelegate CreateMiddleware(EsiRequestDelegate next)
		{
			return async ctx => await _middleware(ctx, next);
		}
	}

	/// <summary>
	///     Creates a Pipeline Wrapper which instantiates the middleware interface, and called it.
	/// </summary>
	private sealed class MiddlewareInterfaceBinder
	{
		private readonly Type _middlewareType;

		public MiddlewareInterfaceBinder(Type middlewareType)
		{
			_middlewareType = middlewareType;
		}

		/// <summary>
		///     Creates a request delegate
		/// </summary>
		/// <param name="next"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public EsiRequestDelegate CreateMiddleware(EsiRequestDelegate next)
		{
			return async ctx =>
			{
				IMiddlewareFactory factory = ctx.ScopedServiceProvider.GetRequiredService<IMiddlewareFactory>();

				IEsiMiddleware? middleware = factory.Create(_middlewareType);

				if (middleware == null)
				{
					throw new InvalidOperationException(
						$"Esi middleware of type {_middlewareType.Name} could not be found in the Service Provider");
				}

				await middleware.HandleAsync(ctx, next, ctx.CancellationToken);
			};
		}
	}
}
