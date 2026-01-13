namespace PureGaze.Application.Requests;

public interface IRequestHandler<in TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken ct);
}

public interface IRequestHandler<in TRequest, TResult> where TRequest : IRequest<TResult>
{
    Task<TResult> Handle(TRequest request, CancellationToken ct);
}
