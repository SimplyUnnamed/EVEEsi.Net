using EveEsi.Net.Config;
using EveEsi.Net.Enums.Client;
using EveEsi.Net.EsiClient;
using EveEsi.Net.Extensions;
using EveEsi.Net.Factories;
using EveEsi.Net.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace EveEsi.Net.UnitTest.Middleware;

public class MiddlewareCollectionTest
{
	private static EsiRequestDelegate Terminal(Func<EsiRequestContext, Task>? onCall = null)
	{
		return async ctx =>
		{
			if (onCall != null)
			{
				await onCall(ctx);
			}
		};
	}

	private static (ServiceProvider sp, IMiddlewareFactory factory) BuildServices(
		Action<IServiceCollection>? extra = null)
	{
		ServiceCollection services = new();
		services.AddSingleton<IMiddlewareFactory, DefaultMiddlewareFactory>();
		extra?.Invoke(services);
		ServiceProvider sp = services.BuildServiceProvider();
		return (sp, sp.GetRequiredService<IMiddlewareFactory>());
	}


	private static EsiRequestContext Ctx(IServiceProvider sp)
	{
		EsiEndpoint testEndpoint = new("random_endpoint", false, HttpMethodType.Get, "/unit/test", null, null);
		return new EsiRequestContext("random_endpoint", testEndpoint, new EsiRequest(), sp)
		{
			CancellationToken = CancellationToken.None
		};
	}

	[Test]
	public async Task MiddlewareCollection_Build_WithoutMiddlewareInvokeTerminal()
	{
		MiddlewareCollection collection = new();
		bool called = false;
		EsiRequestDelegate pipeline = collection.Build(Terminal(_ =>
		{
			called = true;
			return Task.CompletedTask;
		}));

		EsiRequestContext ctx = Ctx(BuildServices().sp);
		await pipeline(ctx);

		called.ShouldBeTrue();
	}

	[Test]
	public async Task MiddlewareCollection_FunctionBinder_RunsInRegistrationOrder()
	{
		List<string> order = new();
		MiddlewareCollection collection = new();
		collection.AddMiddleware((ctx, next) =>
		{
			order.Add("A:Before");
			next(ctx);
			order.Add("A:After");
			return Task.CompletedTask;
		});
		collection.AddMiddleware((ctx, next) =>
		{
			order.Add("B:Before");
			next(ctx);
			order.Add("B:After");
			return Task.CompletedTask;
		});
		EsiRequestDelegate pipeline = collection.Build(Terminal(_ =>
		{
			order.Add("T");
			return Task.CompletedTask;
		}));
		EsiRequestContext ctx = Ctx(BuildServices().sp);
		await pipeline(ctx);

		order.ShouldBe(new[] { "A:Before", "B:Before", "T", "B:After", "A:After" });
	}

	[Test]
	public void MiddlewareCollection_AddMiddleware_ThrowsIfDoesNotImplementIEsiMiddleware()
	{
		IMiddlewareCollection collection = new MiddlewareCollection();
		Should.Throw<InvalidOperationException>(() => collection.AddMiddleware<NotMiddlewareClass>());
		Should.Throw<InvalidOperationException>(() => collection.AddMiddleware(typeof(NotMiddlewareClass)));
	}

	[Test]
	public void MiddlewareCollection_WhenFrozen_BlocksFurtherAdds()
	{
		MiddlewareCollection collection = new();
		collection.Freeze();

		Should.Throw<InvalidOperationException>(() =>
			collection.AddMiddleware(new MiddlewareComponent(next => next, 5)));
	}

	[Test]
	public async Task MiddlewareCollection_Clone_ReturnsIndependentCopy()
	{
		List<string> order = new();
		MiddlewareCollection original = new();
		original.AddMiddleware(async (ctx, next) =>
		{
			order.Add("Original:Before");
			await next(ctx);
			order.Add("Original:After");
		});

		IMiddlewareCollection clone = original.Clone();
		clone.Middleware.Count.ShouldBe(1);

		clone.AddMiddleware(async (ctx, next) =>
		{
			order.Add("Clone:Before");
			await next(ctx);
			order.Add("Clone:After");
		});
		clone.Middleware.Count.ShouldBe(2);
		original.Middleware.Count.ShouldBe(1);
		EsiRequestDelegate pipeline = clone.Build(Terminal(_ =>
		{
			order.Add("T");
			return Task.CompletedTask;
		}));
		EsiRequestContext ctx = Ctx(BuildServices().sp);
		await pipeline(ctx);

		order.ShouldBe(new[] { "Original:Before", "Clone:Before", "T", "Clone:After", "Original:After" });
	}

	[Test]
	public async Task MiddlewareCollection_AddByType_ResolvesFromFactory()
	{
		(ServiceProvider sp, _) = BuildServices(s =>
		{
			s.AddSingleton<SingletonTestObject>();
			s.AddTransient<TestEnvelopMiddleware>();
		});

		MiddlewareCollection collection = new();
		collection.AddMiddleware(typeof(TestEnvelopMiddleware));

		bool terminalHit = false;
		SingletonTestObject testObject = sp.GetRequiredService<SingletonTestObject>();
		EsiRequestDelegate pipeline = collection.Build(Terminal(_ =>
		{
			terminalHit = true;
			return Task.CompletedTask;
		}));
		EsiRequestContext ctx = Ctx(sp);
		await pipeline(ctx);

		terminalHit.ShouldBeTrue();
		testObject.CalledFrom.Contains(nameof(TestEnvelopMiddleware)).ShouldBeTrue();
	}

	// ---------------------- Test doubles -----------------------------------


	// A minimal default factory that uses ActivatorUtilities
	private sealed class DefaultMiddlewareFactory : IMiddlewareFactory
	{
		private readonly IServiceProvider _sp;

		public DefaultMiddlewareFactory(IServiceProvider sp)
		{
			_sp = sp;
		}

		public IEsiMiddleware? Create(Type middlewareType)
		{
			return (IEsiMiddleware?)ActivatorUtilities.CreateInstance(_sp, middlewareType);
		}
	}

	private class NotMiddlewareClass
	{
	}

	private sealed class SingletonTestObject
	{
		public List<string> CalledFrom { get; } = new();
	}

	private sealed class TestEnvelopMiddleware(SingletonTestObject testObject) : IEsiMiddleware
	{
		public Task HandleAsync(EsiRequestContext context, EsiRequestDelegate next,
			CancellationToken cancellationToken = default)
		{
			testObject.CalledFrom.Add(nameof(TestEnvelopMiddleware));
			return next(context);
		}
	}
}
