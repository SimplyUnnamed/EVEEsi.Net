namespace EveEsi.Net.EsiClient;

public interface IEsiRequestClient<TResponseType>
{
	IEsiRequestClient<TResponseType> AddMiddleware<TMiddleware>();
	IEsiRequestClient<TResponseType> AddMiddleware(Type type);
	IEsiRequestClient<TResponseType> AddMiddleware(Func<EsiRequestContext, EsiRequestDelegate, Task> middleware);
	public Task<TResponseType> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IRequestClient<TResponse> : IEsiRequestClient<EsiResponse<TResponse>>;

public interface IRequestClient : IEsiRequestClient<EsiResponse>;
