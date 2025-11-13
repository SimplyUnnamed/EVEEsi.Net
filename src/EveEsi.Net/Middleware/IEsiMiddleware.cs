using EveEsi.Net.EsiClient;

namespace EveEsi.Net.Middleware;

public interface IEsiMiddleware
{
	public Task HandleAsync(EsiRequestContext context, EsiRequestDelegate next,
		CancellationToken cancellationToken = default);
}
