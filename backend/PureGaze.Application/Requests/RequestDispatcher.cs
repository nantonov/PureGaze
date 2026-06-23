using Microsoft.Extensions.DependencyInjection;

namespace PureGaze.Application.Requests;

public class RequestDispatcher(IServiceProvider serviceProvider)
    : IRequestDispatcher
{
    public Task SendAsync<TRequest>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest
    {
        IRequestHandler<TRequest> handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();

        return handler.Handle(request, ct);
    }

    public Task<TResult> SendAsync<TRequest, TResult>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest<TResult>
    {
        IRequestHandler<TRequest, TResult> handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResult>>();

        return handler.Handle(request, ct);
    }
}
