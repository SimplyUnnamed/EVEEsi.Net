# EveEsi.Net

EveEsi.Net is a strongly-typed HTTP client implementation for the  
[EVE Online ESI API](https://developers.eveonline.com/api-explorer).  
It uses a middleware-pipeline architecture, allowing you to register middleware globally or per request, while keeping every request fully typed, composable, and testable.

## ✨ Features

- Strongly typed request/response models for ESI
- Per-endpoint request clients (`ICharacterEndpoints`, `IAssetsEndpoints`, etc.)
- Middleware support (global + per-request)
- Automatic pagination helpers
- Scoped dependency injection inside middleware
- Clean separation between request configuration and execution

## 🚫 Limitations

- **Authentication is not included.**  
  You must supply access tokens per request.  
  (You *can* implement middleware to validate or refresh tokens.)

- **Versioning is based on ESI’s `X-Compatibility-Date`.**  
  Each version of EveEsi.Net is aligned to a specific compatibility date.

## 🚀 Getting Started

### Installation
TODO: To be completed

## ⚙️ Setup

Register the ESI client in your dependency injection container.  
Registration returns an `EveEsiBuilder`, which allows configuration of global middleware and settings.

```csharp
// Basic registration
var esiBuilder = services.ConfigureEveEsiClient(
    name: "My-Esi-App",
    version: "1.0.0"
);

// With configuration callback
var esiBuilder = services.ConfigureEveEsiClient(config =>
{
    // configure Esi Client settings
}, 
name: "My-Esi-App", 
version: "1.0.0");
```

## ⚙️ ESI Client Settings

TODO: complete Configuration Documentation

## 📡 Using the ESI Client

Each ESI operation returns a *request client* that you execute.  
Inject the corresponding endpoint interface and call its method.

### Example: Fetching Character Assets (with pagination)

```csharp
public class MyCode(IAssetsEndpoints assetEndpoints)
{
    public async Task<IEnumerable<CharacterAsset>> GetAllCharacterAssets(
        long characterId,
        string accessToken,
        CancellationToken cancellationToken)
    {
        var assets = new ConcurrentBag<CharacterAsset>();

        // First page
        var client = await assetEndpoints
            .GetCharacterAssets(characterId, accessToken, 1)
            .ExecuteAsync(cancellationToken);

        if (client.TryGetData(out var pageResults, out var _))
        {
            foreach (var item in pageResults)
                assets.Add(item);
        }

        // Additional pages?
        if (client.IsPagedResponse(out var totalPages))
        {
            using var throttle = new SemaphoreSlim(2); // limit concurrency
            var pages = Enumerable.Range(2, (int)totalPages - 1);

            var tasks = pages.Select(async page =>
            {
                await throttle.WaitAsync(cancellationToken);

                try
                {
                    var pageClient = await assetEndpoints
                        .GetCharacterAssets(characterId, accessToken, page)
                        .ExecuteAsync(cancellationinationToken);

                    if (pageClient.TryGetData(out var results, out _))
                    {
                        foreach (var item in results)
                            assets.Add(item);
                    }
                }
                finally
                {
                    throttle.Release();
                }
            });

            await Task.WhenAll(tasks);
        }

        return assets.ToList();
    }
}
```

## 📚 Endpoint Interfaces

Each interface represents a logical ESI group:

- **IAllianceEndpoints**
- **IAssetsEndpoints**
- **ICalendarEndpoints**
- **ICharacterEndpoints**
- **ICloneEndpoints**
- **IContactEndpoints**
- **IContractEndpoints**
- **ICorporationEndpoints**

## 🧩 Middleware

Middleware can be registered **globally** or **per request**.

Two styles are supported:

## 1. Middleware Class

Create a class implementing `IEsiMiddleware`:

```csharp
// MyLoggingMiddleware.cs
public class MyLoggingMiddleware(ILogger<MyLoggingMiddleware> logger) : IEsiMiddleware
{
    public async Task HandleAsync(
        EsiRequestContext context,
        EsiRequestDelegate next,
        CancellationToken cancellationToken = default)
    {
        var requestId = Guid.NewGuid().ToString();
        context.Request.Headers.Add("X-Application-RequestId", requestId);

        using (logger.BeginScope(new { RequestId = requestId }))
        {
            await next(context);

            if (!context.ResponseContext.IsSuccessStatusCode)
            {
                logger.LogError("An error occurred during the ESI request");
            }
        }
    }
}
```

### Register Globally

```csharp
var esiBuilder = services.ConfigureEveEsiClient("My-Esi-App", "1.0.0");
esiBuilder.Use<MyLoggingMiddleware>();
```

### Register Per Request

```csharp
app.MapGet("/character/{characterId:long}", 
    async (long characterId, ICharacterEndpoints characterEndpoints, CancellationToken cancellationToken) =>
{
    var character = await characterEndpoints
        .GetCharacterInfo(characterId)
        .AddMiddleware<MyLoggingMiddleware>()
        .ExecuteAsync(cancellationToken);

    return character;
});
```

## 2. Function Middleware

Inline middleware functions are also supported.

### Global

```csharp
esiBuilder.Use(async (context, next) =>
{
    var logger = context.ScopedServiceProvider.GetRequiredService<ILogger<MyLoggingMiddleware>>();
    var requestId = Guid.NewGuid().ToString();

    context.Request.Headers.Add("X-Application-RequestId", requestId);

    using (logger.BeginScope(new { RequestId = requestId }))
    {
        await next(context);

        if (!context.ResponseContext.IsSuccessStatusCode)
        {
            logger.LogError("An error occurred");
        }
    }
});
```

### Per Request

```csharp
app.MapGet("/character/{characterId:long}", 
    async (long characterId, ICharacterEndpoints endpoints, CancellationToken cancellationToken) =>
{
    var result = await endpoints.GetCharacterInfo(characterId)
        .AddMiddleware(async (context, next) =>
        {
            var logger = context.ScopedServiceProvider.GetRequiredService<ILogger>();
            var requestId = Guid.NewGuid().ToString();

            context.Request.Headers.Add("X-Application-RequestId", requestId);

            using (logger.BeginScope(new { RequestId = requestId }))
            {
                await next(context);

                if (!context.ResponseContext.IsSuccessStatusCode)
                {
                    logger.LogError("An error occurred");
                }
            }
        })
        .ExecuteAsync(cancellationToken);

    return result;
});
```

